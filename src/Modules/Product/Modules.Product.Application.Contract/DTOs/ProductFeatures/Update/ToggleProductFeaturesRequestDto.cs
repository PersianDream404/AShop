using Modules.Product.Application.Contract.DTOs.ProductFeaturess.Create;
using Modules.Product.Application.Contract.Resources.ProductFeatures;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Modules.Product.Application.Contract.DTOs.ProductFeaturess.Toggle;

public class ToggleProductFeaturesRequestDto
{
    [Display(
        Name = nameof(ProductFeaturesFieldNames.Id),
        ResourceType = typeof(ProductFeaturesFieldNames)
    )]
    public long Id { get; set; }
}
