using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Command;
using Framwork.Validation.Resources;
using Mapster;
using Modules.Product.Application.Contract.Interface.FeaturesCategories;
using Modules.Product.Application.Contract.UseCase.FeaturesCategorys.Commands;
using Modules.Product.Domain.Entities.FeaturesCategories;
using Modules.Product.Domain.Interface.FeaturesCategories;
using SharedKernel.Constants;
using SharedKernel.Helper;

namespace Modules.Product.Application.UseCase.FeaturesCategorys.Commands.Create;

public class CreateFeaturesCategoryCommandHandler(IFeaturesCategoryCommandRepository FeaturesCategoryCommandRepository, IFeaturesCategoryQueryRepository featuresCategoryQueryRepository)
: ICommandHandler<CreateFeaturesCategoryCommand, bool>
{
    public async Task<Result<bool>> Handle(
        CreateFeaturesCategoryCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            if (!await featuresCategoryQueryRepository.IsUniqueAsync(x => x.Title == command.request.Title, true,cancellationToken))
                return Result.Error(MessageHelper.Format(AppMessages.Found, AppEntityFeaturesCategory.Title));
            var featuresCategory = command.request.Adapt<FeaturesCategory>();
            await FeaturesCategoryCommandRepository.AddAsync(featuresCategory,cancellationToken);

        }
        catch (Exception)
        {
            return Result.Error(MessageHelper.Format(AppMessages.ErrorIn, AppEntity.FeaturesCategory));
        }

        return true;
    }
}


public class CreateFeaturesCategoryCommandValidator : AbstractValidator<CreateFeaturesCategoryCommand>
{
    public CreateFeaturesCategoryCommandValidator()
    {

        RuleFor(x => x.request.Title)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.Required)
            .MaximumLength(250)
            .WithMessage(SharedValidationMessages.MaxLength);

    }
}