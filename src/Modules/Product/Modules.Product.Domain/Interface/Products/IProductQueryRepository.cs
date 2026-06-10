using Framwork.PagedList;
using Modules.Product.Application.Contract.DTOs.Products;
using SharedKernel.Interface.Repositories;


namespace Modules.Product.Domain.Interface.Products;

public interface IProductQueryRepository : IQueryRepository<Entities.Products.Product>
{
    Task<PagedList<GetAllProductResponseDto>> GetAllAsync(GetAllProductRequestDto request,CancellationToken ct);
}
