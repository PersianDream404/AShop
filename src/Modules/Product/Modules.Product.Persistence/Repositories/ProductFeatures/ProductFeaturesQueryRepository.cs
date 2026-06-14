using Framwork.PagedList;
using Identity.Persistence.Context;
using Infrastructure.Extensions;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Modules.Product.Application.Contract.DTOs.ProductFeaturess.GetAll;
using Modules.Product.Application.Contract.Interface.Features;
using Modules.Product.Domain.Entities.Features;
using Modules.Product.Persistence.Mapper.ProductFeaturess;

namespace Modules.Product.Persistence.Repositories.ProductFeaturess;

public class ProductFeaturesQueryRepository
    : QueryRepository<ProductFeatures>, IProductFeaturesQueryRepository
{
    private readonly ProductReadDbContext _dbContext;
    public ProductFeaturesQueryRepository(ProductReadDbContext context) : base(context)
    {
        _dbContext = context;
    }
    public async Task<PagedList<GetAllProductFeaturesResponseDto>> GetAllProjectedAsync(GetAllProductFeaturesRequestDto request, CancellationToken ct)
    {
        var query = _dbContext.ProductFeatures
            .AsNoTracking()
            .WhereIf(!string.IsNullOrWhiteSpace(request.Q), x => x.Title.Contains(request.Q!));

        var result = await query.ToPagedListAsync(
            ProductFeaturesMapper.ToGetAllDto(),
            request.PageNumber,
            request.PageSize,
            ct);

        return result;
    }

    public async Task<PagedList<GetSelectListProductFeaturesResponseDto>> GetSelectListProjectedAsync(GetSelectListProductFeaturesRequestDto request, CancellationToken ct)
    {
        var query = _dbContext.ProductFeatures
            .AsNoTracking()
            .Where(x=>x.Status)
            .WhereIf(!string.IsNullOrWhiteSpace(request.Q), x => x.Title.Contains(request.Q!));

        var result = await query.ToPagedListAsync(
            ProductFeaturesMapper.ToGetSelectListDto(),
            request.PageNumber,
            request.PageSize,
            ct);

        return result;
    }
    public async Task<GetByIdProductFeaturesResponseDto?> GetByIdProjectedAsync(long id, CancellationToken ct)
    {
        return await _dbContext.ProductFeatures
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(ProductFeaturesMapper.ToGetByIdDto())
            .FirstOrDefaultAsync(ct);
    }

}
