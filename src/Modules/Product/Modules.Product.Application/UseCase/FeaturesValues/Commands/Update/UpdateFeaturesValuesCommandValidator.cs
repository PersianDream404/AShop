using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Command;
using Framwork.Validation.Resources;
using Mapster;
using Modules.Product.Application.Contract.Interface.Categories;
using Modules.Product.Application.Contract.Interface.Features;
using Modules.Product.Application.Contract.UseCase.FeaturesValuess.Commands;
using Modules.Product.Domain.Interface;
using Modules.Product.Domain.Interface.Categories;
using SharedKernel.Constants;
using SharedKernel.Helper;

namespace Modules.Product.Application.UseCase.FeaturesValuess.Commands.Update;

public class UpdateFeaturesValuesCommandHandler(IFeaturesValuesCommandRepository featuresValuesCommandRepository,IFeaturesValuesQueryRepository featuresValuesQueryRepository)
: ICommandHandler<UpdateFeaturesValuesCommand, bool>
{
    public async Task<Result<bool>> Handle(
        UpdateFeaturesValuesCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var featuresValues = await featuresValuesQueryRepository.GetByIdAsync(command.request.Id, cancellationToken);
            if (featuresValues == null)
                return Result.Error(MessageHelper.Format(AppMessages.NotFound, AppEntity.FeaturesValues));

            //if (!await featuresValuesQueryRepository.IsUniqueAsync(x => x.Title == command.request.Title, false, cancellationToken))
            //    return Result.Error(MessageHelper.Format(AppMessages.Found, AppEntityFeaturesValues.Title));
            command.request.Adapt(featuresValues);
            await featuresValuesCommandRepository.UpdateAsync(featuresValues,cancellationToken);

        }
        catch (Exception)
        {
            return Result.Error(MessageHelper.Format(AppMessages.ErrorIn, AppEntity.FeaturesValues));
        }

        return true;
    }
}
public class UpdateFeaturesValuesCommandValidator
    : AbstractValidator<UpdateFeaturesValuesCommand>
{
    public UpdateFeaturesValuesCommandValidator()
    {
        RuleFor(x => x.request.Id)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.Required);
        RuleFor(x => x.request.FeatureValue)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.Required)
            .MaximumLength(250)
            .WithMessage(SharedValidationMessages.MaxLength);

        RuleFor(x => x.request.ProductFeaturesCategoryId)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.Required);

        RuleFor(x => x.request.ProductFeaturesId)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.Required);

    }
}