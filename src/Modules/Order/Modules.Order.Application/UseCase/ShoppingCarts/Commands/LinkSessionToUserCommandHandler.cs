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
    IOrderCommandRepository orderCommandRepository
    )
    : ICommandHandler<LinkSessionToUserCommand, bool>
{
    public async Task<Result<bool>> Handle(LinkSessionToUserCommand command, CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            var newCart = await shoppingCartQueryRepository
                .GetByIdAsync(command.CartId, cancellationToken);

            if (newCart is null)
            {
                await unitOfWork.RollbackAsync(cancellationToken);

                return Result.NotFound(
                    "ShoppingCart.NotFound",
                    "Shopping cart not found");
            }

            if (newCart.UserId.HasValue && newCart.UserId.Value != command.UserId)
            {
                await unitOfWork.RollbackAsync(cancellationToken);
                return Result.Conflict(
                    "ShoppingCart.AlreadyLinked",
                    "این سبد خرید قبلاً به حساب دیگری متصل شده است.");
            }

            var oldCart = await shoppingCartQueryRepository
                .GetByNotIdAsync(
                    command.UserId,
                    command.CartId,
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

                var linkResult = oldCart.LinkToUser(command.UserId);

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
                var linkResult = newCart.LinkToUser(command.UserId);

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
        RuleFor(x => x.CartId)
            .GreaterThan(0)
            .WithMessage(SharedValidationMessages.Invalid);

        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage(SharedValidationMessages.Invalid);
    }
}
