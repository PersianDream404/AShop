using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Command;
using Modules.Product.Application.Contract.Interface.FeaturesCategories;
using Modules.Product.Application.Contract.UseCase.FeaturesCategorys.Commands;
using Modules.Product.Domain.Interface.FeaturesCategories;
using SharedKernel.Constants;
using SharedKernel.Helper;

namespace Modules.Product.Application.UseCase.FeaturesCategorys.Commands.Update;

public class ToggleFeaturesCategoryCommandHandler(IFeaturesCategoryCommandRepository featuresCategoryCommandRepository,IFeaturesCategoryQueryRepository featuresCategoryQueryRepository)
: ICommandHandler<ToggleFeaturesCategoryCommand, bool>
{
    public async Task<Result<bool>> Handle(
        ToggleFeaturesCategoryCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var featuresCategory = await featuresCategoryQueryRepository.GetByIdAsync(command.Id, cancellationToken);
            if (featuresCategory == null)
                return Result.Error(MessageHelper.Format(AppMessages.NotFound, AppEntity.FeaturesCategory));

         
            await featuresCategoryCommandRepository.ToggleAsync(featuresCategory,cancellationToken);

        }
        catch (Exception)
        {
            return Result.Error(MessageHelper.Format(AppMessages.ErrorIn, AppEntity.FeaturesCategory));
        }

        return true;
    }
}
public class ToggleFeaturesCategoryCommandValidator
    : AbstractValidator<ToggleFeaturesCategoryCommand>
{
    public ToggleFeaturesCategoryCommandValidator()
    {

        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("ارسال شناسه الزامب است.");

    }
}