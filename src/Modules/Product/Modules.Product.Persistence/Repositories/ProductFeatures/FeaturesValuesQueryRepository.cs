using Framwork.PagedList;
using Identity.Persistence.Context;
using Infrastructure.Extensions;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Modules.Product.Application.Contract.DTOs.FeaturesValuess.GetAll;
using Modules.Product.Application.Contract.Interface.Features;
using Modules.Product.Domain.Entities.Features;
using Modules.Product.Persistence.Mapper.FeaturesValuess;

namespace Modules.Product.Persistence.Repositories.FeaturesValuess;

public class FeaturesValuesQueryRepository
    : QueryRepository<FeaturesValues>, IFeaturesValuesQueryRepository
{
    private readonly ProductReadDbContext _dbContext;
    public FeaturesValuesQueryRepository(ProductReadDbContext context) : base(context)
    {
        _dbContext = context;
    }
    public async Task<PagedList<GetAllFeaturesValuesResponseDto>> GetAllProjectedAsync(GetAllFeaturesValuesRequestDto request, CancellationToken ct)
    {
        var query = _dbContext.FeaturesValues
            .AsNoTracking()
            .WhereIf(!string.IsNullOrWhiteSpace(request.Q), x => x.FeatureValue.Contains(request.Q!));

        var result = await query.ToPagedListAsync(
            FeaturesValuesMapper.ToGetAllDto(),
            request.PageNumber,
            request.PageSize,
            ct);

        return result;
    }

    public async Task<PagedList<GetSelectListFeaturesValuesResponseDto>> GetSelectListProjectedAsync(GetSelectListFeaturesValuesRequestDto request, CancellationToken ct)
    {
        var query = _dbContext.FeaturesValues
            .AsNoTracking()
            .Where(x=>x.Status)
            .WhereIf(!string.IsNullOrWhiteSpace(request.Q), x => x.FeatureValue.Contains(request.Q!));

        var result = await query.ToPagedListAsync(
            FeaturesValuesMapper.ToGetSelectListDto(),
            request.PageNumber,
            request.PageSize,
            ct);

        return result;
    }
    public async Task<GetByIdFeaturesValuesResponseDto?> GetByIdProjectedAsync(long id, CancellationToken ct)
    {
        return await _dbContext.FeaturesValues
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(FeaturesValuesMapper.ToGetByIdDto())
            .FirstOrDefaultAsync(ct);
    }

}
