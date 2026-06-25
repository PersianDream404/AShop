using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Command;
using Framwork.Validation.Resources;
using Modules.Order.Application.Contract.Interface.Orders;
using Modules.Order.Application.Contract.Interface.ShoppingCarts;
using Modules.Order.Application.Contract.Resources.Orders;
using Modules.Order.Application.Contract.UseCase.ShoppingCarts.Commands;
using Modules.Order.Domain.Interfaces;
using SharedKernel.Interface.Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Modules.Order.Application.UseCase.ShoppingCarts.Commands;

public class LinkSessionToUserCommandHandler(IUnitOfWork unitOfWork,
    IShoppingCartCommandRepository shoppingCartCommandRepository, IShoppingCartQueryRepository shoppingCartQueryRepository,
    IOrderQueryRepository orderQueryRepository, IOrderCommandRepository orderCommandRepository
    )
    : ICommandHandler<LinkSessionToUserCommand, bool>
{
    public async Task<Result<bool>> Handle(LinkSessionToUserCommand command, CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            var request = command.Request;

            var newCart = await shoppingCartQueryRepository
                .GetByIdAsync(request.CartId, cancellationToken);

            if (newCart is null)
            {
                await unitOfWork.RollbackAsync(cancellationToken);

                return Result.NotFound(
                    "ShoppingCart.NotFound",
                    "Shopping cart not found");
            }

            var oldCart = await shoppingCartQueryRepository
                .GetByNotIdAsync(
                    request.UserId,
                    request.CartId,
                    cancellationToken);

            if (oldCart is not null)
            {
                var mergeResult = await orderCommandRepository.MergePendingOrderAsync(
                    targetCartId: oldCart.Id,
                    sourceCartId: newCart.Id,
                    cancellationToken);

                if (!mergeResult.IsSuccess)
                {
                    await unitOfWork.RollbackAsync(cancellationToken);
                    return Result.Conflict("Order.MergeFailed");
                }

                var linkResult = oldCart.LinkToUser(request.UserId);

                if (!linkResult.IsSuccess)
                {
                    await unitOfWork.RollbackAsync(cancellationToken);

                    return Result.Conflict(
                        "ShoppingCart.UpdateFailed",
                        "Failed to update shopping cart");
                }

               await shoppingCartCommandRepository.UpdateAsync(oldCart);
                await shoppingCartCommandRepository.DeleteAsync(newCart);
            }
            else
            {
                var linkResult = newCart.LinkToUser(request.UserId);

                if (!linkResult.IsSuccess)
                {
                    await unitOfWork.RollbackAsync(cancellationToken);

                    return Result.Conflict(
                        "ShoppingCart.UpdateFailed",
                        "Failed to update shopping cart");
                }

                shoppingCartCommandRepository.Update(newCart);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);

            return Result.Success(true);
        }
        catch
        {
            await unitOfWork.RollbackAsync(cancellationToken);
            throw;
        }
    }
}



public class LinkSessionToUserCommandValidator : AbstractValidator<LinkSessionToUserCommand>
{
    public LinkSessionToUserCommandValidator()
    {
        RuleFor(x => x.Request.CartId)
            .GreaterThan(0)
            .WithMessage(SharedValidationMessages.InvalidId);

        RuleFor(x => x.Request.UserId)
            .GreaterThan(0)
            .WithMessage(SharedValidationMessages.InvalidId);
    }
}
