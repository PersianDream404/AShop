using Framwork.PagedList;
using Identity.Persistence.Context;
using Infrastructure.Extensions;
using Infrastructure.Repositories;
using Modules.Product.Application.Contract.DTOs.Products;
using Modules.Product.Domain.Entities.Products;
using Modules.Product.Domain.Interface.Products;

namespace Modules.Product.Persistence.Repositories.Users;

public class ProductQueryRepository
    : QueryRepository<Modules.Product.Domain.Entities.Products.Product>, IProductQueryRepository
{
    private readonly ProductReadDbContext _dbContext;
    public ProductQueryRepository(ProductReadDbContext context) : base(context)
    {
        _dbContext = context;
    }

    public async Task<PagedList<GetAllProductResponseDto>> GetAllAsync(GetAllProductRequestDto request,CancellationToken ct)
    {
        var query = _dbContext.Products
            .AsQueryable()
            .WhereIf(!string.IsNullOrEmpty(request.Q),x=>x.Title.Contains(request.Q!));


        var result = await query.ToPagedListAsync(x => new GetAllProductResponseDto
        {
            Code= x.Code,
            Description= x.Description,
            Id= x.Id,
            Image= x.Image,
            IsActive= x.IsActive,
            Price= x.Price,
            SellCount= x.SellCount,
            ShortDescription= x.ShortDescription,
            Title= x.Title,
            ViewCount = x.ViewCount
        }, request.PageNumber, request.PageSize,ct);
        return result;

    }


}
