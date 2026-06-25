using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Command;
using Framwork.Validation.Resources;
using Mapster;
using Modules.Order.Application.Contract.DTOs;
using Modules.Order.Application.Contract.Interface.ShoppingCarts;
using Modules.Order.Application.Contract.Resources.Orders;
using Modules.Order.Application.Contract.UseCase.Orders.Commands;
using Modules.Order.Domain.Entities;
using Modules.Order.Domain.Enums;
using Modules.Order.Domain.Interfaces;
using SharedKernel.Constants;
using SharedKernel.Helper;
using System.Text.RegularExpressions;

namespace Modules.Order.Application.UseCase.Orders.Commands;

public class CreateOrderCommandHandler(IOrderCommandRepository commandRepository, IShoppingCartQueryRepository cartQueryRepository)
    : ICommandHandler<CreateOrderCommand, bool>
{
    public async Task<Result<bool>> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var cart = await cartQueryRepository.GetByIdAsync(command.Request.ShoppingCartId, cancellationToken);
            if (cart == null)
                return Result.Error(OrderValidationMessages.ShoppingCartNotFound);

            var orderResult = OrderEntity.Create(
                command.Request.ShoppingCartId,
                command.Request.ShippingAddress,
                command.Request.MobileNumber,
                command.Request.TrackingNumber
            );

            if (!orderResult.IsSuccess)
                return Result.Error(orderResult.Errors.FirstOrDefault() ?? OrderValidationMessages.ErrorCreatingOrder);

            await commandRepository.AddAsync(orderResult.Value, cancellationToken);
            return Result.Success(true);
        }
        catch (Exception ex)
        {
            return Result.Error($"{OrderValidationMessages.ErrorCreatingOrder}: {ex.Message}");
        }
    }
}

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.Request.ShoppingCartId)
            .GreaterThan(0)
            .WithMessage(SharedValidationMessages.InvalidId);

        RuleFor(x => x.Request.ShippingAddress)
            .MaximumLength(500)
            .WithMessage(SharedValidationMessages.MaxLength);

        RuleFor(x => x.Request.MobileNumber)
            .MaximumLength(20)
            .WithMessage(SharedValidationMessages.MaxLength)
            .Matches(@"^09\d{9}$", RegexOptions.None)
            .WithMessage(OrderValidationMessages.MobileNumberFormat)
            .When(x => !string.IsNullOrEmpty(x.Request.MobileNumber));
    }
}











