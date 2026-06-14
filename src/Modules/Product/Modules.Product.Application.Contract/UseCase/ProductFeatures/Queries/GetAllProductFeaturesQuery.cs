using Framwork.Bus.Query;
using Framwork.PagedList;
using Modules.Product.Application.Contract.DTOs.ProductFeaturess.GetAll;

namespace Modules.Product.Application.Contract.UseCase.ProductFeaturess.Queries;

public record GetAllProductFeaturesQuery(GetAllProductFeaturesRequestDto request) : 
    IQuery<PagedList<GetAllProductFeaturesResponseDto>>;
public record GetSelectListProductFeaturesQuery(GetSelectListProductFeaturesRequestDto request) :
    IQuery<PagedList<GetSelectListProductFeaturesResponseDto>>;
public record GetByIdProductFeaturesQuery(long Id) : IQuery<GetByIdProductFeaturesResponseDto>;
