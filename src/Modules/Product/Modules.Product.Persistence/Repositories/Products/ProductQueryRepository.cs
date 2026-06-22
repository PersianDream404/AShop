using Framwork.PagedList;
using Identity.Persistence.Context;
using Infrastructure.Extensions;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Modules.Product.Application.Contract.DTOs.Brands.GetAll;
using Modules.Product.Application.Contract.DTOs.Products.Get;
using Modules.Product.Application.Contract.DTOs.Products.GetAll;
using Modules.Product.Application.Contract.Interface.Products;
using Modules.Product.Domain.Entities.Products;
using Modules.Product.Domain.Interface.Products;
using Modules.Product.Persistence.Mapper.Brands;

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


        var result = await query.ToPagedListAsync(ProductMapper.ToGetAllDto(), request.PageNumber, request.PageSize,ct);
        return result;

    }

    public async Task<GetByIdProductResponseDto?> GetByIdProjectedAsync(long Id, CancellationToken ct)
    {
        return await _dbContext.Products
            .AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(ProductMapper.ToGetByIdDto())
            .FirstOrDefaultAsync(ct);
    }

    public async Task<PagedList<GetSelectListProductResponseDto>> GetSelectListAsync(GetSelectListProductRequestDto request, CancellationToken ct)
    {
        var query = _dbContext.Products
      .AsQueryable()
      .WhereIf(!string.IsNullOrEmpty(request.Q), x => x.Title.Contains(request.Q!));


        var result = await query.ToPagedListAsync(ProductMapper.ToGetAllSelectListDto(), request.PageNumber, request.PageSize, ct);
        return result;
    }
}
