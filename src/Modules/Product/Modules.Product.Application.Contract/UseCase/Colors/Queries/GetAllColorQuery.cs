using Framwork.Bus.Query;
using Framwork.PagedList;
using Modules.Product.Application.Contract.DTOs.Colors.GetAll;

namespace Modules.Product.Application.Contract.UseCase.Colors.Queries;

public record GetAllColorQuery(GetAllColorRequestDto request) : 
    IQuery<PagedList<GetAllColorResponseDto>>;
public record GetSelectListColorQuery(GetSelectListColorRequestDto request) :
    IQuery<PagedList<GetSelectListColorResponseDto>>;
public record GetByIdColorQuery(long Id) : IQuery<GetByIdColorResponseDto>;
