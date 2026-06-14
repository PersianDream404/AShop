using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Query;
using Framwork.PagedList;
using Modules.Product.Application.Contract.DTOs.FeaturesCategorys.GetAll;
using Modules.Product.Application.Contract.Interface.FeaturesCategories;
using Modules.Product.Application.Contract.UseCase.FeaturesCategorys.Queries;
using SharedKernel.Constants;
using SharedKernel.Helper;

namespace Modules.Product.Application.UseCase.FeaturesCategorys.Queries.GetAll;

public class GetFeaturesCategoryQueryHandler(IFeaturesCategoryQueryRepository FeaturesCategoryQueryRepository)
: IQueryHandler<GetAllFeaturesCategoryQuery, PagedList<GetAllFeaturesCategoryResponseDto>>
{
    public async Task<Result<PagedList<GetAllFeaturesCategoryResponseDto>>> Handle(
        GetAllFeaturesCategoryQuery query,
        CancellationToken cancellationToken)
    {

        try
        {
            var result =await FeaturesCategoryQueryRepository.GetAllProjectedAsync(query.request, cancellationToken);

            return result;
        }
        catch (Exception)
        {
            return Result.Error(MessageHelper.Format(AppMessages.ErrorIn, AppEntity.FeaturesCategory));
        }

    }
}
public class GetAllFeaturesCategoryQueryValidator
    : AbstractValidator<GetAllFeaturesCategoryQuery>
{
    public GetAllFeaturesCategoryQueryValidator()
    {
        //RuleFor(x => x.request.Q)
        //    .NotEmpty()
        //    .WithMessage("FeaturesCategory Id is required");
    }
}
