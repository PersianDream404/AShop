using Modules.Product.Application.Contract.Resources.FeaturesValues;
using Modules.Product.Application.Contract.Resources.ProductFeatures;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Modules.Product.Application.Contract.DTOs.ProductFeaturess.Create;

public class CreateProductFeaturesRequestDto
{

    /// <summary>
    /// نام 
    /// </summary>

    [Display(
        Name = nameof(ProductFeaturesFieldNames.Title),
        ResourceType = typeof(ProductFeaturesFieldNames)
    )]
    public string Title { get; set; } = null!;





}
