using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Product.Persistence.Mapper.FeaturesValuess;

using Modules.Product.Application.Contract.DTOs.FeaturesValuess.GetAll;
using Modules.Product.Domain.Entities.Categories;
using Modules.Product.Domain.Entities.Features;
using System.Linq.Expressions;

public static class FeaturesValuesMapper
{
    public static Expression<Func<FeaturesValues, GetByIdFeaturesValuesResponseDto>> ToGetByIdDto()
    {
        return x => new GetByIdFeaturesValuesResponseDto
        {
            Id = x.Id,
            FeatureValue = x.FeatureValue,
            ProductFeaturesId = x.ProductFeaturesId,
            ProductFeaturesTitle = x.ProductFeatures!.Title,

            ProductFeaturesCategoryId = x.ProductFeaturesCategoryId,
            ProductFeaturesCategoryTitle = x.ProductFeaturesCategory!.Title,
            Status = x.Status
        };
    }

    public static Expression<Func<FeaturesValues, GetAllFeaturesValuesResponseDto>> ToGetAllDto()
    {
        return x => new GetAllFeaturesValuesResponseDto
        {
            Id = x.Id,
            ProductFeaturesId = x.ProductFeaturesId,
            FeatureValue = x.FeatureValue,
            ProductFeaturesCategoryId = x.ProductFeaturesCategoryId,
            ProductFeaturesTitle = x.ProductFeatures!.Title,
            ProductFeaturesCategoryTitle = x.ProductFeaturesCategory!.Title,
            Status = x.Status
        };
    }

    public static Expression<Func<FeaturesValues, GetSelectListFeaturesValuesResponseDto>> ToGetSelectListDto()
    {
        return x => new GetSelectListFeaturesValuesResponseDto
        {
            Id = x.Id,

            Title = x.ProductFeatures!.Title + " " + x.FeatureValue,
            Status = x.Status
        };
    }
}
