using Framwork.Bus.Query;
using Framwork.PagedList;
using Modules.Product.Application.Contract.DTOs.Brands.GetAll;

namespace Modules.Product.Application.Contract.UseCase.Brands.Queries;

public record GetAllBrandQuery(GetAllBrandRequestDto request) : 
    IQuery<PagedList<GetAllBrandResponseDto>>;
public record GetSelectListBrandQuery(GetSelectListBrandRequestDto request) :
    IQuery<PagedList<GetSelectListBrandResponseDto>>;
public record GetByIdBrandQuery(long Id) : IQuery<GetByIdBrandResponseDto>;
