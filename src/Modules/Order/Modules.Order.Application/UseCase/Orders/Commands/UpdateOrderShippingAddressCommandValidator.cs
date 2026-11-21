using FluentValidation;
using Framwork.Validation.Resources;
using Modules.Order.Application.Contract.UseCase.Orders.Commands;

namespace Modules.Order.Application.UseCase.Orders.Commands;

public sealed class UpdateOrderShippingAddressCommandValidator
    : AbstractValidator<UpdateOrderShippingAddressCommand>
{
    public UpdateOrderShippingAddressCommandValidator()
    {
        RuleFor(command => command.UserId)
            .GreaterThan(0)
            .WithMessage(SharedValidationMessages.Invalid);

        RuleFor(command => command.OrderId)
            .GreaterThan(0)
            .WithMessage(SharedValidationMessages.Invalid);

        RuleFor(command => command.AddressId)
            .GreaterThan(0)
            .WithMessage(SharedValidationMessages.Invalid);
    }
}
