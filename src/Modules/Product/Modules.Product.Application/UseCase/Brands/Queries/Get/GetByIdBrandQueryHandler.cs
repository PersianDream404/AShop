using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Query;
using Modules.Product.Application.Contract.DTOs.Brands.GetAll;
using Modules.Product.Application.Contract.Interface.Brands;
using Modules.Product.Application.Contract.UseCase.Brands.Queries;
using SharedKernel.Constants;
using SharedKernel.Helper;

namespace Modules.Product.Application.UseCase.Brands.Queries.Get;

public class GetByIdBrandQueryHandler(IBrandQueryRepository BrandQueryRepository)
: IQueryHandler<GetByIdBrandQuery, GetByIdBrandResponseDto>
{
    public async Task<Result<GetByIdBrandResponseDto>> Handle(
        GetByIdBrandQuery query,
        CancellationToken cancellationToken)
    {

        try
        {
            var brand = await BrandQueryRepository.GetByIdProjectedAsync(query.Id, cancellationToken);
            if (brand == null)
                return Result.Error(MessageHelper.Format(AppMessages.NotFound, AppEntity.Brand));
            return brand;
        }
        catch (Exception)
        {
            return Result.Error(MessageHelper.Format(AppMessages.ErrorIn, AppEntity.Brand));
        }

    }
}
public class GetAllBrandQueryValidator
    : AbstractValidator<GetAllBrandQuery>
{
    public GetAllBrandQueryValidator()
    {
        //RuleFor(x => x.request.Q)
        //    .NotEmpty()
        //    .WithMessage("Brand Id is required");
    }
}
