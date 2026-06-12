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

public class GetSelectListBrandQueryHandler(IBrandQueryRepository BrandQueryRepository)
: IQueryHandler<GetSelectListBrandQuery, PagedList<GetSelectListBrandResponseDto>>
{
    public async Task<Result<PagedList<GetSelectListBrandResponseDto>>> Handle(
        GetSelectListBrandQuery query,
        CancellationToken cancellationToken)
    {

        try
        {
            var result =await BrandQueryRepository.GetSelectListProjectedAsync(query.request, cancellationToken);

            return result;
        }
        catch (Exception)
        {
            return Result.Error(MessageHelper.Format(AppMessages.ErrorIn, AppEntity.Brand));
        }

    }
}
public class GetSelectListBrandQueryValidator
    : AbstractValidator<GetSelectListBrandQuery>
{
    public GetSelectListBrandQueryValidator()
    {
        //RuleFor(x => x.request.Q)
        //    .NotEmpty()
        //    .WithMessage("Brand Id is required");
    }
}
