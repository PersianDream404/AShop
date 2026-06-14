using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Product.Persistence.Mapper.ProductFeaturess;

using Modules.Product.Application.Contract.DTOs.ProductFeaturess.GetAll;
using Modules.Product.Domain.Entities.Features;
using System.Linq.Expressions;

public static class ProductFeaturesMapper
{
    public static Expression<Func<ProductFeatures, GetByIdProductFeaturesResponseDto>> ToGetByIdDto()
    {
        return x => new GetByIdProductFeaturesResponseDto
        {
            Id = x.Id,
            Title = x.Title,
            Status = x.Status
        };
    }

    public static Expression<Func<ProductFeatures, GetAllProductFeaturesResponseDto>> ToGetAllDto()
    {
        return x => new GetAllProductFeaturesResponseDto
        {
            Id = x.Id,
            
            Title = x.Title,
            Status = x.Status
        };
    }

    public static Expression<Func<ProductFeatures, GetSelectListProductFeaturesResponseDto>> ToGetSelectListDto()
    {
        return x => new GetSelectListProductFeaturesResponseDto
        {
            Id = x.Id,
          
            Title = x.Title,
            Status = x.Status
        };
    }
}
