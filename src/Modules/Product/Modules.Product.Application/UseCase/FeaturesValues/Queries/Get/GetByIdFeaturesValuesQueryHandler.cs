using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Query;
using Modules.Product.Application.Contract.DTOs.FeaturesValuess.GetAll;
using Modules.Product.Application.Contract.Interface.Categories;
using Modules.Product.Application.Contract.Interface.Features;
using Modules.Product.Application.Contract.UseCase.FeaturesValuess.Queries;
using SharedKernel.Constants;
using SharedKernel.Helper;

namespace Modules.Product.Application.UseCase.FeaturesValuess.Queries.Get;

public class GetByIdFeaturesValuesQueryHandler(IFeaturesValuesQueryRepository FeaturesValuesQueryRepository)
: IQueryHandler<GetByIdFeaturesValuesQuery, GetByIdFeaturesValuesResponseDto>
{
    public async Task<Result<GetByIdFeaturesValuesResponseDto>> Handle(
        GetByIdFeaturesValuesQuery query,
        CancellationToken cancellationToken)
    {

        try
        {
            var featuresValues = await FeaturesValuesQueryRepository.GetByIdProjectedAsync(query.Id, cancellationToken);
            if (featuresValues == null)
                return Result.Error(MessageHelper.Format(AppMessages.NotFound, AppEntity.FeaturesValues));
            return featuresValues;
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
