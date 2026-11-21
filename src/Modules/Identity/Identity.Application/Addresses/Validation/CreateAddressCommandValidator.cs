using FluentValidation;
using Identity.Application.Contract.UseCase.Addresses.Commands;

namespace Identity.Application.Addresses.Validation;

public sealed class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
{
    public CreateAddressCommandValidator()
        => RuleFor(x => x.Request).SetValidator(new AddressRequestValidator());
}
