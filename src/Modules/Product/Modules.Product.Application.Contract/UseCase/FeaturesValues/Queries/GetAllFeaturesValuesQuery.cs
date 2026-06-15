using Framwork.Bus.Query;
using Framwork.PagedList;
using Modules.Product.Application.Contract.DTOs.FeaturesValuess.GetAll;

namespace Modules.Product.Application.Contract.UseCase.FeaturesValuess.Queries;

public record GetAllFeaturesValuesQuery(GetAllFeaturesValuesRequestDto request) : 
    IQuery<PagedList<GetAllFeaturesValuesResponseDto>>;
public record GetSelectListFeaturesValuesQuery(GetSelectListFeaturesValuesRequestDto request) :
    IQuery<PagedList<GetSelectListFeaturesValuesResponseDto>>;
public record GetByIdFeaturesValuesQuery(long Id) : IQuery<GetByIdFeaturesValuesResponseDto>;
