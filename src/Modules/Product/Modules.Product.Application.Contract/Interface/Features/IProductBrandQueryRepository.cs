using Framwork.PagedList;
using Modules.Product.Application.Contract.DTOs.Categorys.GetAll;
using Modules.Product.Application.Contract.DTOs.FeaturesValuess.GetAll;
using Modules.Product.Application.Contract.DTOs.ProductFeaturess.GetAll;
using Modules.Product.Domain.Entities.Features;
using SharedKernel.Interface.Repositories;


namespace Modules.Product.Application.Contract.Interface.Features;

public interface IProductFeaturesQueryRepository : IQueryRepository<ProductFeatures>
{
    Task<PagedList<GetAllProductFeaturesResponseDto>> GetAllProjectedAsync(GetAllProductFeaturesRequestDto request, CancellationToken ct);
    Task<PagedList<GetSelectListProductFeaturesResponseDto>> GetSelectListProjectedAsync(GetSelectListProductFeaturesRequestDto request, CancellationToken ct);
    Task<GetByIdProductFeaturesResponseDto?> GetByIdProjectedAsync(long id, CancellationToken ct);


}
public interface IFeaturesValuesQueryRepository : IQueryRepository<FeaturesValues>
{
    Task<PagedList<GetAllFeaturesValuesResponseDto>> GetAllProjectedAsync(GetAllFeaturesValuesRequestDto request, CancellationToken ct);
    Task<PagedList<GetSelectListFeaturesValuesResponseDto>> GetSelectListProjectedAsync(GetSelectListFeaturesValuesRequestDto request, CancellationToken ct);
    Task<GetByIdFeaturesValuesResponseDto?> GetByIdProjectedAsync(long id, CancellationToken ct);


}
