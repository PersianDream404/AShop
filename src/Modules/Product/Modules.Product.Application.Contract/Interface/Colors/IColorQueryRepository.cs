using Framwork.PagedList;
using Modules.Product.Application.Contract.DTOs.Colors.GetAll;
using Modules.Product.Domain.Entities.Colors;
using SharedKernel.Interface.Repositories;


namespace Modules.Product.Application.Contract.Interface.Colors;

public interface IColorQueryRepository : IQueryRepository<Color>
{
    Task<PagedList<GetAllColorResponseDto>> GetAllProjectedAsync(GetAllColorRequestDto request, CancellationToken ct);
    Task<PagedList<GetSelectListColorResponseDto>> GetSelectListProjectedAsync(GetSelectListColorRequestDto request, CancellationToken ct);
    Task<GetByIdColorResponseDto?> GetByIdProjectedAsync(long id, CancellationToken ct);
}
