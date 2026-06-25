using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Command;
using Framwork.Validation.Resources;
using Modules.Order.Application.Contract.Interface.ShoppingCarts;
using Modules.Order.Application.Contract.Resources.Orders;
using Modules.Order.Application.Contract.UseCase.ShoppingCarts.Commands;
using Modules.Order.Domain.Interfaces;

namespace Modules.Order.Application.UseCase.ShoppingCarts.Commands;

public class LinkSessionToUserCommandHandler(IShoppingCartCommandRepository commandRepository, IShoppingCartQueryRepository queryRepository)
    : ICommandHandler<LinkSessionToUserCommand, bool>
{
    public async Task<Result<bool>> Handle(LinkSessionToUserCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var cart = await queryRepository.GetByIdAsync(command.Request.CartId, cancellationToken);
            if (cart == null)
                return Result.Error(OrderValidationMessages.ShoppingCartNotFound);

            var linkResult = cart.LinkToUser(command.Request.UserId);
            if (!linkResult.IsSuccess)
                return Result.Error(linkResult.Errors.FirstOrDefault()?.ErrorMessage ?? OrderValidationMessages.ErrorLinkingSessionToUser);

            await commandRepository.UpdateAsync(cart, cancellationToken);
            return Result.Success(true);
        }
        catch (Exception ex)
        {
            return Result.Error($"{OrderValidationMessages.ErrorLinkingSessionToUser}: {ex.Message}");
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
