using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Query;
using Modules.Product.Application.Contract.DTOs.ProductFeaturess.GetAll;
using Modules.Product.Application.Contract.Interface.Features;
using Modules.Product.Application.Contract.UseCase.ProductFeaturess.Queries;
using SharedKernel.Constants;
using SharedKernel.Helper;

namespace Modules.Product.Application.UseCase.ProductFeaturess.Queries.Get;

public class GetByIdProductFeaturesQueryHandler(IProductFeaturesQueryRepository ProductFeaturesQueryRepository)
: IQueryHandler<GetByIdProductFeaturesQuery, GetByIdProductFeaturesResponseDto>
{
    public async Task<Result<GetByIdProductFeaturesResponseDto>> Handle(
        GetByIdProductFeaturesQuery query,
        CancellationToken cancellationToken)
    {

        try
        {
            var productFeatures = await ProductFeaturesQueryRepository.GetByIdProjectedAsync(query.Id, cancellationToken);
            if (productFeatures == null)
                return Result.Error(MessageHelper.Format(AppMessages.NotFound, AppEntity.ProductFeatures));
            return productFeatures;
        }
        catch (Exception)
        {
            return Result.Error(MessageHelper.Format(AppMessages.ErrorIn, AppEntity.ProductFeatures));
        }

    }
}
public class GetAllProductFeaturesQueryValidator
    : AbstractValidator<GetAllProductFeaturesQuery>
{
    public GetAllProductFeaturesQueryValidator()
    {
        //RuleFor(x => x.request.Q)
        //    .NotEmpty()
        //    .WithMessage("ProductFeatures Id is required");
    }
}
