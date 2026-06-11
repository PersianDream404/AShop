using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Query;
using Framwork.PagedList;
using Modules.Product.Application.Contract.DTOs.Brands.GetAll;
using Modules.Product.Application.Contract.Interface.Brands;
using Modules.Product.Application.Contract.UseCase.Brands.Queries;
using SharedKernel.Constants;
using SharedKernel.Helper;

namespace Modules.Product.Application.UseCase.Brands.Queries.GetAll;

public class GetBrandQueryHandler(IBrandQueryRepository BrandQueryRepository)
: IQueryHandler<GetAllBrandQuery, PagedList<GetAllBrandResponseDto>>
{
    public async Task<Result<PagedList<GetAllBrandResponseDto>>> Handle(
        GetAllBrandQuery query,
        CancellationToken cancellationToken)
    {

        try
        {
            var result =await BrandQueryRepository.GetAllProjectedAsync(query.request, cancellationToken);

            return result;
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
