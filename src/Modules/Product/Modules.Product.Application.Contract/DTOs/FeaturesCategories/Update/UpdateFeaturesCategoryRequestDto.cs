using Modules.Product.Application.Contract.DTOs.FeaturesCategorys.Create;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Product.Application.Contract.DTOs.FeaturesCategorys.Update;

public class UpdateFeaturesCategoryRequestDto: CreateFeaturesCategoryRequestDto
{
    public long Id { get; set; }
}
