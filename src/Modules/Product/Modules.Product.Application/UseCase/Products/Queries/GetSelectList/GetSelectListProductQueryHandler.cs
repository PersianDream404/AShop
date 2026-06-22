using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Query;
using Framwork.PagedList;
using Modules.Product.Application.Contract.DTOs.Products.GetAll;
using Modules.Product.Application.Contract.Interface.Products;
using Modules.Product.Application.Contract.UseCase.Products.Queries;
using SharedKernel.Constants;
using SharedKernel.Helper;

namespace Modules.Product.Application.UseCase.Products.Queries.GetSelectList;

public class GetSelectListProductQueryHandler(IProductQueryRepository ProductQueryRepository)
: IQueryHandler<GetSelectListProductQuery, PagedList<GetSelectListProductResponseDto>>
{
    public async Task<Result<PagedList<GetSelectListProductResponseDto>>> Handle(
        GetSelectListProductQuery query,
        CancellationToken cancellationToken)
    {

        try
        {
            var result =await ProductQueryRepository.GetSelectListAsync(query.request, cancellationToken);

            return result;
        }
        catch (Exception)
        {
            return Result.Error(MessageHelper.Format(AppMessages.ErrorIn, AppEntity.Product));
        }

    }
}
public class GetSelectListProductQueryValidator
    : AbstractValidator<GetSelectListProductQuery>
{
    public GetSelectListProductQueryValidator()
    {
        //RuleFor(x => x.request.Q)
        //    .NotEmpty()
        //    .WithMessage("Product Id is required");
    }
}
