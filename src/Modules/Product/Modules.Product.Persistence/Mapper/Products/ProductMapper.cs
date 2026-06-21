using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Product.Persistence.Mapper.Brands;

using Modules.Product.Application.Contract.DTOs.Brands.GetAll;
using Modules.Product.Application.Contract.DTOs.Products.Get;
using Modules.Product.Application.Contract.DTOs.Products.GetAll;
using System.Linq.Expressions;

public static class ProductMapper
{
    public static Expression<Func<Domain.Entities.Products.Product, GetByIdProductResponseDto>> ToGetByIdDto()
    {
        return x => new GetByIdProductResponseDto
        {
            Id = x.Id,
            Image = x.Image,
            Code = x.Code,
            Description = x.Description,
            Price = x.Price,
            ShortDescription = x.ShortDescription,
            Title = x.Title,
            ViewCount = x.ViewCount,
            IsActive=x.Status,
            Brands=x.ProductSelectedBrands.Select(z=> new GetByIdProductBrandsSelectListResponseDto(z.ProductBrandId,z.ProductBrand.Title)),
            Categories=x.ProductSelectedCategories.Select(z=> new GetByIdProductCategoriesSelectListResponseDto(z.ProductCategoryId,z.ProductCategory.Title)),
            Colors=x.ProductSelectedColors.Select(z=> new GetByIdProductColorsSelectListResponseDto(z.ProductColorId,z.ProductColor.ColorName)),
            ProductFeatures=x.ProductFeatures.Select(z=> new GetByIdProductSelectedFeaturesRequestDto(z.Id,z.ProductFeaturesCategoryId,z.ProductFeaturesId,z.FeatureValue)),
            ProductGalleries=x.ProductGalleries.Select(z=> new GetByIdProductGalleryRequestDto(z.Id,z.DisplayPriority,z.ImageName)),
        };
    }

    public static Expression<Func<Domain.Entities.Products.Product, GetAllProductResponseDto>> ToGetAllDto()
    {
        return x => new GetAllProductResponseDto
        {
            Id = x.Id,
            Image = x.Image,
            Code = x.Code,
            Description = x.Description,
            Price = x.Price,
            ShortDescription = x.ShortDescription,
            Title = x.Title,
            ViewCount = x.ViewCount,
            //SellCount=x.
        };
    }


}
