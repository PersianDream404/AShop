using Framwork.PagedList;
using Identity.Persistence.Context;
using Infrastructure.Extensions;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Modules.Product.Application.Contract.DTOs.Categorys.GetAll;
using Modules.Product.Application.Contract.Interface.Categories;
using Modules.Product.Application.Contract.Interface.Features;
using Modules.Product.Domain.Entities.Categories;
using Modules.Product.Domain.Entities.Features;
using Modules.Product.Persistence.Mapper.Categorys;

namespace Modules.Product.Persistence.Repositories.Categorys;

public class CategoryQueryRepository
    : QueryRepository<Category>, ICategoryQueryRepository
{
    private readonly ProductReadDbContext _dbContext;
    public CategoryQueryRepository(ProductReadDbContext context) : base(context)
    {
        _dbContext = context;
    }
    public async Task<PagedList<GetAllCategoryResponseDto>> GetAllProjectedAsync(GetAllCategoryRequestDto request, CancellationToken ct)
    {
        var query = _dbContext.Category
            .AsNoTracking()
            .WhereIf(!string.IsNullOrWhiteSpace(request.Q), x => x.Title.Contains(request.Q!));

        var result = await query.ToPagedListAsync(
            CategoryMapper.ToGetAllDto(),
            request.PageNumber,
            request.PageSize,
            ct);

        return result;
    }

    public async Task<PagedList<GetSelectListCategoryResponseDto>> GetSelectListProjectedAsync(GetSelectListCategoryRequestDto request, CancellationToken ct)
    {
        var query = _dbContext.Category
            .AsNoTracking()
            .Where(x=>x.Status)
            .WhereIf(!string.IsNullOrWhiteSpace(request.Q), x => x.Title.Contains(request.Q!));

        var result = await query.ToPagedListAsync(
            CategoryMapper.ToGetSelectListDto(),
            request.PageNumber,
            request.PageSize,
            ct);

        return result;
    }
    public async Task<GetByIdCategoryResponseDto?> GetByIdProjectedAsync(long id, CancellationToken ct)
    {
        return await _dbContext.Category
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(CategoryMapper.ToGetByIdDto())
            .FirstOrDefaultAsync(ct);
    }

}
