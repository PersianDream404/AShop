using Framwork.PagedList;
using Identity.Persistence.Context;
using Infrastructure.Extensions;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Modules.Product.Application.Contract.DTOs.Colors.GetAll;
using Modules.Product.Application.Contract.Interface.Colors;
using Modules.Product.Domain.Entities.Colors;
using Modules.Product.Persistence.Mapper.Colors;

namespace Modules.Product.Persistence.Repositories.Colors;

public class ColorQueryRepository
    : QueryRepository<Domain.Entities.Colors.Color>, IColorQueryRepository
{
    private readonly ProductReadDbContext _dbContext;
    public ColorQueryRepository(ProductReadDbContext context) : base(context)
    {
        _dbContext = context;
    }
    public async Task<PagedList<GetAllColorResponseDto>> GetAllProjectedAsync(GetAllColorRequestDto request, CancellationToken ct)
    {
        var query = _dbContext.Colors
            .AsNoTracking()
            .WhereIf(!string.IsNullOrWhiteSpace(request.Q), x => x.ColorName.Contains(request.Q!));

        var result = await query.ToPagedListAsync(
            ColorMapper.ToGetAllDto(),
            request.PageNumber,
            request.PageSize,
            ct);

        return result;
    }

    public async Task<PagedList<GetSelectListColorResponseDto>> GetSelectListProjectedAsync(GetSelectListColorRequestDto request, CancellationToken ct)
    {
        var query = _dbContext.Colors
            .AsNoTracking()
            .Where(x=>x.Status)
            .WhereIf(!string.IsNullOrWhiteSpace(request.Q), x => x.ColorName.Contains(request.Q!));

        var result = await query.ToPagedListAsync(
            ColorMapper.ToGetSelectListDto(),
            request.PageNumber,
            request.PageSize,
            ct);

        return result;
    }
    public async Task<GetByIdColorResponseDto?> GetByIdProjectedAsync(long id, CancellationToken ct)
    {
        return await _dbContext.Colors
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(ColorMapper.ToGetByIdDto())
            .FirstOrDefaultAsync(ct);
    }

}
