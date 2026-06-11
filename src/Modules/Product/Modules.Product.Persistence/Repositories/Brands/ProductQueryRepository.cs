using Framwork.PagedList;
using Identity.Persistence.Context;
using Infrastructure.Extensions;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Modules.Product.Application.Contract.DTOs.Brands.GetAll;
using Modules.Product.Application.Contract.Interface.Brands;
using Modules.Product.Persistence.Mapper.Brands;

namespace Modules.Product.Persistence.Repositories.Brands;

public class BrandQueryRepository
    : QueryRepository<Domain.Entities.Brands.Brand>, IBrandQueryRepository
{
    private readonly ProductReadDbContext _dbContext;
    public BrandQueryRepository(ProductReadDbContext context) : base(context)
    {
        _dbContext = context;
    }

    public async Task<PagedList<GetAllBrandResponseDto>> GetAllProjectedAsync(GetAllBrandRequestDto request, CancellationToken ct)
    {
        var query = _dbContext.Brands
            .AsNoTracking()
            .WhereIf(!string.IsNullOrWhiteSpace(request.Q), x => x.Title.Contains(request.Q!));

        var result = await query.ToPagedListAsync(
            BrandMapper.ToGetAllDto(),
            request.PageNumber,
            request.PageSize,
            ct);

        return result;
    }
    public async Task<GetByIdBrandResponseDto?> GetByIdProjectedAsync(long id, CancellationToken ct)
    {
        return await _dbContext.Brands
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(BrandMapper.ToGetByIdDto())
            .FirstOrDefaultAsync(ct);
    }

}
