using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Product.Application.Contract.DTOs.FeaturesValuess.Create;

using Modules.Product.Application.Contract.Resources;
using Modules.Product.Application.Contract.Resources.FeaturesValues;
using System.ComponentModel.DataAnnotations;

public class CreateFeaturesValuesRequestDto
{
    /// <summary>
    /// شناسه دسته‌بندی ویژگی (مثلاً: سخت‌افزار، نمایشگر)
    /// </summary>
    [Display(
        Name = nameof(FeaturesValuesFieldNames.ProductFeaturesCategoryId),
        ResourceType = typeof(FeaturesValuesFieldNames)
    )]
    public long? ProductFeaturesCategoryId { get; set; }

    /// <summary>
    /// عنوان ویژگی (مثلاً: RAM)
    /// </summary>
    [Display(
        Name = nameof(FeaturesValuesFieldNames.ProductFeaturesId),
        ResourceType = typeof(FeaturesValuesFieldNames)
    )]
    public long? ProductFeaturesId { get; set; }

    /// <summary>
    /// مقدار ویژگی (مثلاً: 16GB)
    /// </summary>
    [Display(
        Name = nameof(FeaturesValuesFieldNames.FeatureValue),
        ResourceType = typeof(FeaturesValuesFieldNames)
    )]
    public string FeatureValue { get; set; } = null!;
}
