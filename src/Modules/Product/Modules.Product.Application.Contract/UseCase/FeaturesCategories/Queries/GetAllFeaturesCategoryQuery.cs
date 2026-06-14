using Framwork.Bus.Query;
using Framwork.PagedList;
using Modules.Product.Application.Contract.DTOs.FeaturesCategorys.GetAll;

namespace Modules.Product.Application.Contract.UseCase.FeaturesCategorys.Queries;

public record GetAllFeaturesCategoryQuery(GetAllFeaturesCategoryRequestDto request) : 
    IQuery<PagedList<GetAllFeaturesCategoryResponseDto>>;
public record GetSelectListFeaturesCategoryQuery(GetSelectListFeaturesCategoryRequestDto request) :
    IQuery<PagedList<GetSelectListFeaturesCategoryResponseDto>>;
public record GetByIdFeaturesCategoryQuery(long Id) : IQuery<GetByIdFeaturesCategoryResponseDto>;
