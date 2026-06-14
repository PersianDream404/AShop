using Framwork.PagedList;
using Modules.Product.Application.Contract.DTOs.FeaturesCategorys.GetAll;
using Modules.Product.Domain.Entities.FeaturesCategories;
using SharedKernel.Interface.Repositories;


namespace Modules.Product.Application.Contract.Interface.FeaturesCategories;

public interface IFeaturesCategoryQueryRepository : IQueryRepository<FeaturesCategory>
{
    Task<PagedList<GetAllFeaturesCategoryResponseDto>> GetAllProjectedAsync(GetAllFeaturesCategoryRequestDto request, CancellationToken ct);
    Task<PagedList<GetSelectListFeaturesCategoryResponseDto>> GetSelectListProjectedAsync(GetSelectListFeaturesCategoryRequestDto request, CancellationToken ct);
    Task<GetByIdFeaturesCategoryResponseDto?> GetByIdProjectedAsync(long id, CancellationToken ct);
}
