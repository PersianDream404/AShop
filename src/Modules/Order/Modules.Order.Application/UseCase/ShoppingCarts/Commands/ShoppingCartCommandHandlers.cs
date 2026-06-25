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
            var cartResult = ShoppingCart.Create(command.Request.SessionId, command.Request.UserId);
            if (!cartResult.IsSuccess)
                return Result.Error(cartResult.Errors.FirstOrDefault() ?? OrderValidationMessages.ErrorCreatingShoppingCart);

            await commandRepository.AddAsync(cartResult.Value, cancellationToken);
            return Result.Success(true);
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

