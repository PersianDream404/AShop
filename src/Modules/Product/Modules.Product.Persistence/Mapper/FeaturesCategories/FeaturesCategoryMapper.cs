using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Product.Persistence.Mapper.FeaturesCategorys;

using Modules.Product.Application.Contract.DTOs.FeaturesCategorys.GetAll;
using Modules.Product.Domain.Entities.FeaturesCategories;
using System.Linq.Expressions;

public static class FeaturesCategoryMapper
{
    public static Expression<Func<FeaturesCategory, GetByIdFeaturesCategoryResponseDto>> ToGetByIdDto()
    {
        return x => new GetByIdFeaturesCategoryResponseDto
        {
            Id = x.Id,
            Title = x.Title,
       
       
            Status = x.Status
        };
    }

    public static Expression<Func<FeaturesCategory, GetAllFeaturesCategoryResponseDto>> ToGetAllDto()
    {
        return x => new GetAllFeaturesCategoryResponseDto
        {
            Id = x.Id,
            Title = x.Title,


            Status = x.Status
        };
    }

    public static Expression<Func<FeaturesCategory, GetSelectListFeaturesCategoryResponseDto>> ToGetSelectListDto()
    {
        return x => new GetSelectListFeaturesCategoryResponseDto
        {
            Id = x.Id,
            Title = x.Title,


            Status = x.Status
        };
    }
}
