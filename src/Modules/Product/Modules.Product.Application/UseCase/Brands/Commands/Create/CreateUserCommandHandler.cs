using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Command;
using Framwork.Validation.Resources;
using Mapster;
using Modules.Product.Application.Contract.Interface.Brands;
using Modules.Product.Application.Contract.UseCase.Brands.Commands;
using Modules.Product.Domain.Entities.Brands;
using Modules.Product.Domain.Interface.Brands;
using SharedKernel.Constants;
using SharedKernel.Helper;

namespace Modules.Product.Application.UseCase.Brands.Commands.Create;

public class CreateBrandCommandHandler(IBrandCommandRepository BrandCommandRepository, IBrandQueryRepository brandQueryRepository)
: ICommandHandler<CreateBrandCommand, bool>
{
    public async Task<Result<bool>> Handle(
        CreateBrandCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            if (!await brandQueryRepository.IsUniqueAsync(x => x.UrlName == command.request.UrlName,true,cancellationToken))
                return Result.Error(MessageHelper.Format(AppMessages.Found, AppEntityBrand.UrlName));
            var brand = command.request.Adapt<Brand>();
            await BrandCommandRepository.AddAsync(brand,cancellationToken);

        }
        catch (Exception)
        {
            return Result.Error(MessageHelper.Format(AppMessages.ErrorIn, AppEntity.Brand));
        }

        return true;
    }
}


public class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
{
    public CreateBrandCommandValidator()
    {
        RuleFor(x => x.request.Title)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.Required)
            .MaximumLength(250)
            .WithMessage(SharedValidationMessages.MaxLength);

        RuleFor(x => x.request.UrlName)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.Required)
            .MaximumLength(250)
            .WithMessage(SharedValidationMessages.MaxLength)
            .Matches("^[a-z0-9-]+$")
            .WithMessage(SharedValidationMessages.UrlSlugPattern);

        RuleFor(x => x.request.Image)
            .MaximumLength(250)
            .When(x => !string.IsNullOrWhiteSpace(x.request.Image))
            .WithMessage(SharedValidationMessages.MaxLength);

        RuleFor(x => x.request.Icon)
            .MaximumLength(250)
            .When(x => !string.IsNullOrWhiteSpace(x.request.Icon))
            .WithMessage(SharedValidationMessages.MaxLength);

        RuleFor(x => x.request.ParentId)
            .GreaterThan(0)
            .When(x => x.request.ParentId.HasValue)
            .WithMessage(SharedValidationMessages.InvalidId);
    }
}
