using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Query;
using Framwork.PagedList;
using Modules.Product.Application.Contract.DTOs.Products;
using Modules.Product.Application.Contract.Interface.Products;
using Modules.Product.Application.Contract.UseCase.Products.Queries;
using Modules.Product.Domain.Interface;
using Modules.Product.Domain.Interface.Products;
using SharedKernel.Constants;
using SharedKernel.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Product.Application.UseCase.Products.Queries.GetAll;

public class GetProductQueryHandler(IProductQueryRepository ProductQueryRepository)
: IQueryHandler<GetAllProductQuery, PagedList<GetAllProductResponseDto>>
{
    public async Task<Result<PagedList<GetAllProductResponseDto>>> Handle(
        GetAllProductQuery query,
        CancellationToken cancellationToken)
    {

        try
        {
            var res =await ProductQueryRepository.GetAllAsync(query.request, cancellationToken);

            return res;
        }
        catch (Exception)
        {
            return Result.Error(MessageHelper.Format(AppMessages.ErrorIn, AppEntity.Product));
        }

    }
}
public class GetAllProductQueryValidator
    : AbstractValidator<GetAllProductQuery>
{
    public GetAllProductQueryValidator()
    {
        //RuleFor(x => x.request.Q)
        //    .NotEmpty()
        //    .WithMessage("Product Id is required");
    }
}