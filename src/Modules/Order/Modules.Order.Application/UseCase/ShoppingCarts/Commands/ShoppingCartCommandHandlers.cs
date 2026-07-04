using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Command;
using Framwork.Validation.Resources;
using Mapster;
using Modules.Order.Application.Contract.DTOs;
using Modules.Order.Application.Contract.Interface.ShoppingCarts;
using Modules.Order.Application.Contract.Resources.Orders;
using Modules.Order.Application.Contract.UseCase.ShoppingCarts.Commands;
using Modules.Order.Domain.Entities;
using Modules.Order.Domain.Interfaces;
using SharedKernel.Constants;
using SharedKernel.Helper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Modules.Order.Application.UseCase.ShoppingCarts.Commands;

public class CreateShoppingCartCommandHandler(IShoppingCartCommandRepository commandRepository,IShoppingCartQueryRepository shoppingCartQueryRepository)
    : ICommandHandler<CreateShoppingCartCommand, long>
{
    public async Task<Result<long>> Handle(CreateShoppingCartCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var cart = await shoppingCartQueryRepository.AnyBySessionIdAsync(command.Request.SessionId);

            if (cart)
            {
                return Result.Conflict(OrderValidationMessages.ShoppingCartFound);
            }
            var cartResult = ShoppingCart.Create(command.Request.SessionId, command.Request.UserId);
            if (!cartResult.IsSuccess)
                return Result.Error(cartResult.Errors.FirstOrDefault() ?? OrderValidationMessages.ErrorCreatingShoppingCart);

            await commandRepository.AddAsync(cartResult.Value, cancellationToken);
            return Result.Success(cartResult.Value.Id);
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
            .NotEmpty()
            .WithMessage(SharedValidationMessages.Invalid);
    }
}

