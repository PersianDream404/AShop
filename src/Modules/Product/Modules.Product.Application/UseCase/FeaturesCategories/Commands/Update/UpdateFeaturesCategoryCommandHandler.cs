using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Command;
using Mapster;
using Modules.Product.Application.Contract.Interface.FeaturesCategories;
using Modules.Product.Application.Contract.UseCase.FeaturesCategorys.Commands;
using Modules.Product.Domain.Interface.FeaturesCategories;
using SharedKernel.Constants;
using SharedKernel.Helper;

namespace Modules.Product.Application.UseCase.FeaturesCategorys.Commands.Update;

public class UpdateFeaturesCategoryCommandHandler(IFeaturesCategoryCommandRepository featuresCategoryCommandRepository,IFeaturesCategoryQueryRepository featuresCategoryQueryRepository)
: ICommandHandler<UpdateFeaturesCategoryCommand, bool>
{
    public async Task<Result<bool>> Handle(
        UpdateFeaturesCategoryCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var featuresCategory = await featuresCategoryQueryRepository.GetByIdAsync(command.request.Id, cancellationToken);
            if (featuresCategory == null)
                return Result.Error(MessageHelper.Format(AppMessages.NotFound, AppEntity.FeaturesCategory));

            if (!await featuresCategoryQueryRepository.IsUniqueAsync(x => x.Title == command.request.Title, false, cancellationToken))
                return Result.Error(MessageHelper.Format(AppMessages.Found, AppEntityFeaturesCategory.Title));
            command.request.Adapt(featuresCategory);
            await featuresCategoryCommandRepository.UpdateAsync(featuresCategory,cancellationToken);

        }
        catch (Exception)
        {
            return Result.Error(MessageHelper.Format(AppMessages.ErrorIn, AppEntity.FeaturesCategory));
        }

        return true;
    }
}
public class UpdateFeaturesCategoryCommandValidator
    : AbstractValidator<UpdateFeaturesCategoryCommand>
{
    public UpdateFeaturesCategoryCommandValidator()
    {

        RuleFor(x => x.request.Id)
            .NotEmpty()
            .WithMessage("ارسال شناسه الزامی است.");

        RuleFor(x => x.request.Title)
             .NotEmpty()
             .WithMessage("  نام  الزامی است.")
             .MaximumLength(250)
             .WithMessage(" نام نمی‌تواند بیشتر از ۲۵۰ کاراکتر باشد.");
    }
}