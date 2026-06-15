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

public class GetFeaturesValuesQueryHandler(IFeaturesValuesQueryRepository FeaturesValuesQueryRepository)
: IQueryHandler<GetAllFeaturesValuesQuery, PagedList<GetAllFeaturesValuesResponseDto>>
{
    public async Task<Result<PagedList<GetAllFeaturesValuesResponseDto>>> Handle(
        GetAllFeaturesValuesQuery query,
        CancellationToken cancellationToken)
    {

        try
        {
            var result =await FeaturesValuesQueryRepository.GetAllProjectedAsync(query.request, cancellationToken);

            return result;
        }
        catch (Exception)
        {
            return Result.Error(MessageHelper.Format(AppMessages.ErrorIn, AppEntity.FeaturesValues));
        }

    }
}
public class GetAllFeaturesValuesQueryValidator
    : AbstractValidator<GetAllFeaturesValuesQuery>
{
    public GetAllFeaturesValuesQueryValidator()
    {
        //RuleFor(x => x.request.Q)
        //    .NotEmpty()
        //    .WithMessage("FeaturesValues Id is required");
    }
}
