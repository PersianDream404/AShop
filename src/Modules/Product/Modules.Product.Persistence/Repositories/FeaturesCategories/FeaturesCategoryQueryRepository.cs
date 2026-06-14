using Framwork.PagedList;
using Identity.Persistence.Context;
using Infrastructure.Extensions;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Modules.Product.Application.Contract.DTOs.FeaturesCategorys.GetAll;
using Modules.Product.Application.Contract.Interface.FeaturesCategories;
using Modules.Product.Domain.Entities.FeaturesCategories;
using Modules.Product.Persistence.Mapper.FeaturesCategorys;

namespace Modules.Product.Persistence.Repositories.FeaturesCategorys;

public class FeaturesCategoryQueryRepository
    : QueryRepository<FeaturesCategory>, IFeaturesCategoryQueryRepository
{
    private readonly ProductReadDbContext _dbContext;
    public FeaturesCategoryQueryRepository(ProductReadDbContext context) : base(context)
    {
        _dbContext = context;
    }
    public async Task<PagedList<GetAllFeaturesCategoryResponseDto>> GetAllProjectedAsync(GetAllFeaturesCategoryRequestDto request, CancellationToken ct)
    {
        var query = _dbContext.FeaturesCategory
            .AsNoTracking()
            .WhereIf(!string.IsNullOrWhiteSpace(request.Q), x => x.Title.Contains(request.Q!));

        var result = await query.ToPagedListAsync(
            FeaturesCategoryMapper.ToGetAllDto(),
            request.PageNumber,
            request.PageSize,
            ct);

        return result;
    }

    public async Task<PagedList<GetSelectListFeaturesCategoryResponseDto>> GetSelectListProjectedAsync(GetSelectListFeaturesCategoryRequestDto request, CancellationToken ct)
    {
        var query = _dbContext.FeaturesCategory
            .AsNoTracking()
            .Where(x=>x.Status)
            .WhereIf(!string.IsNullOrWhiteSpace(request.Q), x => x.Title.Contains(request.Q!));

        var result = await query.ToPagedListAsync(
            FeaturesCategoryMapper.ToGetSelectListDto(),
            request.PageNumber,
            request.PageSize,
            ct);

        return result;
    }
    public async Task<GetByIdFeaturesCategoryResponseDto?> GetByIdProjectedAsync(long id, CancellationToken ct)
    {
        return await _dbContext.FeaturesCategory
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(FeaturesCategoryMapper.ToGetByIdDto())
            .FirstOrDefaultAsync(ct);
    }

}
