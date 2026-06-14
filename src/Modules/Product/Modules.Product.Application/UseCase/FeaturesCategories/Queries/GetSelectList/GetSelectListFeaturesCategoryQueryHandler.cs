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

public class GetSelectListFeaturesCategoryQueryHandler(IFeaturesCategoryQueryRepository FeaturesCategoryQueryRepository)
: IQueryHandler<GetSelectListFeaturesCategoryQuery, PagedList<GetSelectListFeaturesCategoryResponseDto>>
{
    public async Task<Result<PagedList<GetSelectListFeaturesCategoryResponseDto>>> Handle(
        GetSelectListFeaturesCategoryQuery query,
        CancellationToken cancellationToken)
    {

        try
        {
            var result =await FeaturesCategoryQueryRepository.GetSelectListProjectedAsync(query.request, cancellationToken);

            return result;
        }
        catch (Exception)
        {
            return Result.Error(MessageHelper.Format(AppMessages.ErrorIn, AppEntity.FeaturesCategory));
        }

    }
}
public class GetSelectListFeaturesCategoryQueryValidator
    : AbstractValidator<GetSelectListFeaturesCategoryQuery>
{
    public GetSelectListFeaturesCategoryQueryValidator()
    {
        //RuleFor(x => x.request.Q)
        //    .NotEmpty()
        //    .WithMessage("FeaturesCategory Id is required");
    }
}
