using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Command;
using Framwork.Validation.Resources;
using Modules.Product.Application.Contract.Interface.FeaturesCategories;
using Modules.Product.Application.Contract.UseCase.FeaturesCategorys.Commands;
using Modules.Product.Domain.Interface.FeaturesCategories;
using SharedKernel.Constants;
using SharedKernel.Helper;

namespace Modules.Product.Application.UseCase.FeaturesCategorys.Commands.Delete;

public class DeleteFeaturesCategoryCommandHandler(IFeaturesCategoryCommandRepository featuresCategoryCommandRepository,IFeaturesCategoryQueryRepository featuresCategoryQueryRepository)
: ICommandHandler<DeleteFeaturesCategoryCommand, bool>
{
    public async Task<Result<bool>> Handle(
        DeleteFeaturesCategoryCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var featuresCategory = await featuresCategoryQueryRepository.GetByIdProjectedAsync(command.Id, cancellationToken);
            if (featuresCategory == null)
                return Result.Error(MessageHelper.Format(AppMessages.NotFound, AppEntity.FeaturesCategory));
        }
        catch (Exception)
        {
            return Result.Error(MessageHelper.Format(AppMessages.ErrorIn, AppEntity.FeaturesCategory));
        }

        return true;
    }
}
public class DeleteFeaturesCategoryCommandValidator
    : AbstractValidator<DeleteFeaturesCategoryCommand>
{
    public DeleteFeaturesCategoryCommandValidator()
    {

        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.Required);



    }
}