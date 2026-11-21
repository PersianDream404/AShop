using FluentValidation;
using Identity.Application.Contract.UseCase.Addresses.Commands;

namespace Identity.Application.Addresses.Validation;

public sealed class UpdateAddressCommandValidator : AbstractValidator<UpdateAddressCommand>
{
    public UpdateAddressCommandValidator()
        => RuleFor(x => x.Request).SetValidator(new AddressRequestValidator());
}
