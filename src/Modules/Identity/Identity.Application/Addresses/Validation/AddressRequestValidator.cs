using FluentValidation;
using Identity.Application.Contract.DTOs.Addresses;

namespace Identity.Application.Addresses.Validation;

public sealed class AddressRequestValidator : AbstractValidator<AddressRequestDto>
{
    public AddressRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
        RuleFor(x => x.RecipientName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Province).NotEmpty().MaximumLength(100);
        RuleFor(x => x.City).NotEmpty().MaximumLength(100);
        RuleFor(x => x.PostalCode).NotEmpty().MaximumLength(20);
        RuleFor(x => x.AddressLine).NotEmpty().MaximumLength(500);
        RuleFor(x => x.MobileNumber).NotEmpty().MaximumLength(20);
    }
}
