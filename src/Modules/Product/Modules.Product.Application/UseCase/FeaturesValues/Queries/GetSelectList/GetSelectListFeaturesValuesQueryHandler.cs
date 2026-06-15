using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Query;
using Framwork.PagedList;
using Modules.Product.Application.Contract.DTOs.FeaturesValuess.GetAll;
using Modules.Product.Application.Contract.Interface.Categories;
using Modules.Product.Application.Contract.Interface.Features;
using Modules.Product.Application.Contract.UseCase.FeaturesValuess.Queries;
using SharedKernel.Constants;
using SharedKernel.Helper;

namespace Modules.Product.Application.UseCase.FeaturesValuess.Queries.GetAll;

public class GetSelectListFeaturesValuesQueryHandler(IFeaturesValuesQueryRepository FeaturesValuesQueryRepository)
: IQueryHandler<GetSelectListFeaturesValuesQuery, PagedList<GetSelectListFeaturesValuesResponseDto>>
{
    public async Task<Result<PagedList<GetSelectListFeaturesValuesResponseDto>>> Handle(
        GetSelectListFeaturesValuesQuery query,
        CancellationToken cancellationToken)
    {

        try
        {
            var result =await FeaturesValuesQueryRepository.GetSelectListProjectedAsync(query.request, cancellationToken);

            return result;
        }
        catch (Exception)
        {
            return Result.Error(MessageHelper.Format(AppMessages.ErrorIn, AppEntity.FeaturesValues));
        }

    }
}
public class GetSelectListFeaturesValuesQueryValidator
    : AbstractValidator<GetSelectListFeaturesValuesQuery>
{
    public GetSelectListFeaturesValuesQueryValidator()
    {
        //RuleFor(x => x.request.Q)
        //    .NotEmpty()
        //    .WithMessage("FeaturesValues Id is required");
    }
}
