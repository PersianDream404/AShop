using Modules.Product.Application.Contract.DTOs.ProductFeaturess.Create;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Product.Application.Contract.DTOs.ProductFeaturess.Update;

public class UpdateProductFeaturesRequestDto: CreateProductFeaturesRequestDto
{
    public long Id { get; set; }
}
