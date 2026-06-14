using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Product.Persistence.Mapper.Colors;

using Modules.Product.Application.Contract.DTOs.Colors.GetAll;
using System.Linq.Expressions;

public static class ColorMapper
{
    public static Expression<Func<Domain.Entities.Colors.Color, GetByIdColorResponseDto>> ToGetByIdDto()
    {
        return x => new GetByIdColorResponseDto
        {
            Id = x.Id,
            ColorCode = x.ColorCode,
            ColorName = x.ColorName,
       
            Status = x.Status
        };
    }

    public static Expression<Func<Domain.Entities.Colors.Color, GetAllColorResponseDto>> ToGetAllDto()
    {
        return x => new GetAllColorResponseDto
        {
            Id = x.Id,
            ColorCode = x.ColorCode,
            ColorName = x.ColorName,
         
            Status = x.Status
        };
    }

    public static Expression<Func<Domain.Entities.Colors.Color, GetSelectListColorResponseDto>> ToGetSelectListDto()
    {
        return x => new GetSelectListColorResponseDto
        {
            Id = x.Id,
            ColorCode = x.ColorCode,
            ColorName = x.ColorName,
       
            Status=x.Status
        };
    }
}
