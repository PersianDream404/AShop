using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Product.Persistence.Mapper.Brands;

using Modules.Product.Application.Contract.DTOs.Brands.GetAll;
using System.Linq.Expressions;

public static class BrandMapper
{
    public static Expression<Func<Domain.Entities.Brands.Brand, GetByIdBrandResponseDto>> ToGetByIdDto()
    {
        return x => new GetByIdBrandResponseDto
        {
            Id = x.Id,
            ParentId = x.ParentId,
            Title = x.Title,
            Image = x.Image,
            UrlName = x.UrlName,
            Icon = x.Icon,
            Status = x.Status
        };
    }

    public static Expression<Func<Domain.Entities.Brands.Brand, GetAllBrandResponseDto>> ToGetAllDto()
    {
        return x => new GetAllBrandResponseDto
        {
            Id = x.Id,
            ParentId = x.ParentId,
            Title = x.Title,
            Image = x.Image,
            UrlName = x.UrlName,
            Icon = x.Icon,
            Status = x.Status
        };
    }

    public static Expression<Func<Domain.Entities.Brands.Brand, GetSelectListBrandResponseDto>> ToGetSelectListDto()
    {
        return x => new GetSelectListBrandResponseDto
        {
            Id = x.Id,
            Title = x.Title,
            Image = x.Image,
            Icon = x.Icon,
        };
    }
}
