using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Query;
using Framwork.PagedList;
using Modules.Product.Application.Contract.DTOs.ProductFeaturess.GetAll;
using Modules.Product.Application.Contract.Interface.Features;
using Modules.Product.Application.Contract.UseCase.ProductFeaturess.Queries;
using SharedKernel.Constants;
using SharedKernel.Helper;

namespace Modules.Product.Application.UseCase.ProductFeaturess.Queries.GetAll;

public class GetSelectListProductFeaturesQueryHandler(IProductFeaturesQueryRepository ProductFeaturesQueryRepository)
: IQueryHandler<GetSelectListProductFeaturesQuery, PagedList<GetSelectListProductFeaturesResponseDto>>
{
    public async Task<Result<PagedList<GetSelectListProductFeaturesResponseDto>>> Handle(
        GetSelectListProductFeaturesQuery query,
        CancellationToken cancellationToken)
    {

        try
        {
            var result =await ProductFeaturesQueryRepository.GetSelectListProjectedAsync(query.request, cancellationToken);

            return result;
        }
        catch (Exception)
        {
            return Result.Error(MessageHelper.Format(AppMessages.ErrorIn, AppEntity.ProductFeatures));
        }

    }
}
public class GetSelectListProductFeaturesQueryValidator
    : AbstractValidator<GetSelectListProductFeaturesQuery>
{
    public GetSelectListProductFeaturesQueryValidator()
    {
        //RuleFor(x => x.request.Q)
        //    .NotEmpty()
        //    .WithMessage("ProductFeatures Id is required");
    }
}
