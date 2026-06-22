using Framwork.Bus.Query;
using Framwork.PagedList;
using Modules.Product.Application.Contract.DTOs.Products.Get;
using Modules.Product.Application.Contract.DTOs.Products.GetAll;

namespace Modules.Product.Application.Contract.UseCase.Products.Queries;

public record GetAllProductQuery(GetAllProductRequestDto request) : IQuery<PagedList<GetAllProductResponseDto>>;
public record GetSelectListProductQuery(GetSelectListProductRequestDto request) :
    IQuery<PagedList<GetSelectListProductResponseDto>>;
public record GetByIdProductQuery(long Id) : IQuery<GetByIdProductResponseDto>;