using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Query;
using Framwork.Validation.Resources;
using Modules.Product.Application.Contract.DTOs.FeaturesCategorys.GetAll;
using Modules.Product.Application.Contract.Interface.FeaturesCategories;
using Modules.Product.Application.Contract.UseCase.FeaturesCategorys.Queries;
using SharedKernel.Constants;
using SharedKernel.Helper;

namespace Modules.Product.Application.UseCase.FeaturesCategorys.Queries.Get;

public class GetByIdFeaturesCategoryQueryHandler(IFeaturesCategoryQueryRepository FeaturesCategoryQueryRepository)
: IQueryHandler<GetByIdFeaturesCategoryQuery, GetByIdFeaturesCategoryResponseDto>
{
    public async Task<Result<GetByIdFeaturesCategoryResponseDto>> Handle(
        GetByIdFeaturesCategoryQuery query,
        CancellationToken cancellationToken)
    {

        try
        {
            var featuresCategory = await FeaturesCategoryQueryRepository.GetByIdProjectedAsync(query.Id, cancellationToken);
            if (featuresCategory == null)
                return Result.Error(MessageHelper.Format(AppMessages.NotFound, AppEntity.FeaturesCategory));
            return featuresCategory;
        }
        catch (Exception)
        {
            return Result.Error(MessageHelper.Format(AppMessages.ErrorIn, AppEntity.FeaturesCategory));
        }

    }
}
public class GetAllFeaturesCategoryQueryValidator
    : AbstractValidator<GetByIdFeaturesCategoryQuery>
{
    public GetAllFeaturesCategoryQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.Required);
    }
}
