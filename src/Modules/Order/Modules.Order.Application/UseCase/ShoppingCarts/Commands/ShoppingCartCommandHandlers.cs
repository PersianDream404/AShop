using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Command;
using Framwork.Validation.Resources;
using Mapster;
using Modules.Order.Application.Contract.DTOs;
using Modules.Order.Application.Contract.Resources.Orders;
using Modules.Order.Application.Contract.UseCase.ShoppingCarts.Commands;
using Modules.Order.Domain.Entities;
using Modules.Order.Domain.Interfaces;
using SharedKernel.Constants;
using SharedKernel.Helper;

namespace Modules.Order.Application.UseCase.ShoppingCarts.Commands;

public class CreateShoppingCartCommandHandler(IShoppingCartCommandRepository commandRepository)
    : ICommandHandler<CreateShoppingCartCommand, bool>
{
    public async Task<Result<bool>> Handle(CreateShoppingCartCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var cart = ShoppingCart.Create(command.Request.SessionId, command.Request.UserId);
            await commandRepository.AddAsync(cart, cancellationToken);
            return Result.Success(true);
        }
        catch (ArgumentException ex)
        {
            return Result.Error(ex.Message);
        }
        catch (Exception ex)
        {
            return Result.Error($"{OrderValidationMessages.ErrorCreatingShoppingCart}: {ex.Message}");
        }
    }
}

public class CreateShoppingCartCommandValidator : AbstractValidator<CreateShoppingCartCommand>
{
    public CreateShoppingCartCommandValidator()
    {
        RuleFor(x => x.Request.SessionId)
            .GreaterThan(0)
            .WithMessage(SharedValidationMessages.InvalidId);
    }
}

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

            cart.LinkToUser(command.Request.UserId);
            await commandRepository.UpdateAsync(cart, cancellationToken);
            return Result.Success(true);
        }
        catch (ArgumentException ex)
        {
            return Result.Error(ex.Message);
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
