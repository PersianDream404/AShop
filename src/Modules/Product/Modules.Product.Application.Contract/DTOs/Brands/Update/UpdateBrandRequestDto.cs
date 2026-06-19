using Modules.Product.Application.Contract.DTOs.Brands.Create;
using Modules.Product.Application.Contract.Resources.Brands;
using Modules.Product.Application.Contract.Resources.Categories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Modules.Product.Application.Contract.DTOs.Brands.Update;

public class UpdateBrandRequestDto: CreateBrandRequestDto
{
    [Display(
    Name = nameof(BrandFieldNames.Id),
    ResourceType = typeof(BrandFieldNames)
)]
    public long Id { get; set; }
}
