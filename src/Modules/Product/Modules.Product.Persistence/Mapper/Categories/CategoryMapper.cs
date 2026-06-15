using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Product.Persistence.Mapper.Categorys;

using Modules.Product.Application.Contract.DTOs.Categorys.GetAll;
using Modules.Product.Domain.Entities.Categories;
using Modules.Product.Domain.Entities.Features;
using System.Linq.Expressions;

public static class CategoryMapper
{
    public static Expression<Func<Category, GetByIdCategoryResponseDto>> ToGetByIdDto()
    {
        return x => new GetByIdCategoryResponseDto
        {
            Id = x.Id,
            Icon = x.Icon,
            Image = x.Image,
            ParentId = x.ParentId,
            UrlName = x.UrlName,
            Title = x.Title,
            Status = x.Status
        };
    }

    public static Expression<Func<Category, GetAllCategoryResponseDto>> ToGetAllDto()
    {
        return x => new GetAllCategoryResponseDto
        {
            Id = x.Id,
            Icon   =x.Icon,
            Image =x.Image,
            ParentId = x.ParentId,
            UrlName=x.UrlName,
            Title = x.Title,
            Status = x.Status
        };
    }

    public static Expression<Func<Category, GetSelectListCategoryResponseDto>> ToGetSelectListDto()
    {
        return x => new GetSelectListCategoryResponseDto
        {
            Id = x.Id,
          
            Title = x.Title,
            Status = x.Status
        };
    }
}
