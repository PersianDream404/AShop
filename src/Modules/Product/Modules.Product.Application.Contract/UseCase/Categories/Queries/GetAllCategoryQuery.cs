using Framwork.Bus.Query;
using Framwork.PagedList;
using Modules.Product.Application.Contract.DTOs.Categorys.GetAll;

namespace Modules.Product.Application.Contract.UseCase.Categorys.Queries;

public record GetAllCategoryQuery(GetAllCategoryRequestDto request) : 
    IQuery<PagedList<GetAllCategoryResponseDto>>;
public record GetSelectListCategoryQuery(GetSelectListCategoryRequestDto request) :
    IQuery<PagedList<GetSelectListCategoryResponseDto>>;
public record GetByIdCategoryQuery(long Id) : IQuery<GetByIdCategoryResponseDto>;
