using Modules.Product.Application.Contract.DTOs.FeaturesCategorys.Create;
using Modules.Product.Application.Contract.Resources.FeaturesCategories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Modules.Product.Application.Contract.DTOs.FeaturesCategorys.Toggle;

public class ToggleFeaturesCategoryRequestDto
{
    [Display(
        Name = nameof(FeaturesCategoriesFieldNames.Id),
        ResourceType = typeof(FeaturesCategoriesFieldNames)
    )]
    public long Id { get; set; }
}
