using Framwork.PagedList;
using Modules.Product.Application.Contract.DTOs.FeaturesCategorys.GetAll;
using Modules.Product.Application.Contract.DTOs.Categorys.GetAll;
using Modules.Product.Domain.Entities.Categories;
using SharedKernel.Interface.Repositories;


namespace Modules.Product.Application.Contract.Interface.Categories;

public interface ICategoryQueryRepository : IQueryRepository<Category>
{
    Task<PagedList<GetAllCategoryResponseDto>> GetAllProjectedAsync(GetAllCategoryRequestDto request, CancellationToken ct);
    Task<PagedList<GetSelectListCategoryResponseDto>> GetSelectListProjectedAsync(GetSelectListCategoryRequestDto request, CancellationToken ct);
    Task<GetByIdCategoryResponseDto?> GetByIdProjectedAsync(long id, CancellationToken ct);
}
