using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Product.Application.Contract.DTOs.FeaturesCategorys.Create;

using Modules.Product.Application.Contract.Resources;
using Modules.Product.Application.Contract.Resources.FeaturesCategories;
using System.ComponentModel.DataAnnotations;

public class CreateFeaturesCategoryRequestDto
{
    /// <summary>
    /// نام
    /// </summary>
    [Display(
        Name = nameof(FeaturesCategoriesFieldNames.Title),
        ResourceType = typeof(FeaturesCategoriesFieldNames)
    )]
    public string Title { get; set; } = null!;
}
