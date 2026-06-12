using Framwork.PagedList;
using Modules.Product.Application.Contract.DTOs.Brands.GetAll;
using Modules.Product.Application.Contract.DTOs.Products;
using Modules.Product.Domain.Entities.Brands;
using SharedKernel.Interface.Repositories;


namespace Modules.Product.Application.Contract.Interface.Brands;

public interface IBrandQueryRepository : IQueryRepository<Brand>
{
    Task<PagedList<GetAllBrandResponseDto>> GetAllProjectedAsync(GetAllBrandRequestDto request, CancellationToken ct);
    Task<PagedList<GetSelectListBrandResponseDto>> GetSelectListProjectedAsync(GetSelectListBrandRequestDto request, CancellationToken ct);
    Task<GetByIdBrandResponseDto?> GetByIdProjectedAsync(long id, CancellationToken ct);
}
