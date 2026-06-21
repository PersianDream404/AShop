using Framwork.PagedList;
using Modules.Product.Application.Contract.DTOs.Brands.GetAll;
using Modules.Product.Application.Contract.DTOs.Products.Get;
using Modules.Product.Application.Contract.DTOs.Products.GetAll;
using SharedKernel.Interface.Repositories;


namespace Modules.Product.Application.Contract.Interface.Products;

public interface IProductQueryRepository : IQueryRepository<Modules.Product.Domain.Entities.Products.Product>
{
    Task<PagedList<GetAllProductResponseDto>> GetAllAsync(GetAllProductRequestDto request,CancellationToken ct);
    Task<GetByIdProductResponseDto?> GetByIdProjectedAsync(int Id, CancellationToken ct);
}
