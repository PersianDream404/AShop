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

namespace Modules.Product.Application.UseCase.Brands.Commands.Update;

public class UpdateBrandCommandHandler(IBrandCommandRepository brandCommandRepository,IBrandQueryRepository brandQueryRepository)
: ICommandHandler<UpdateBrandCommand, bool>
{
    public async Task<Result<bool>> Handle(
        UpdateBrandCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var brand = await brandQueryRepository.GetByIdAsync(command.request.Id, cancellationToken);
            if (brand == null)
                return Result.Error(MessageHelper.Format(AppMessages.NotFound, AppEntity.Brand));

            if (!await brandQueryRepository.IsUniqueAsync(x => x.UrlName == command.request.UrlName, false, cancellationToken))
                return Result.Error(MessageHelper.Format(AppMessages.Found, AppEntityBrand.UrlName));
            command.request.Adapt(brand);
            await brandCommandRepository.UpdateAsync(brand,cancellationToken);

        }
        catch (Exception)
        {
            return Result.Error(MessageHelper.Format(AppMessages.ErrorIn, AppEntity.Brand));
        }

        return true;
    }
}
public class UpdateBrandCommandValidator
    : AbstractValidator<UpdateBrandCommand>
{
    public UpdateBrandCommandValidator()
    {

        RuleFor(x => x.request.Id)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.Required);


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