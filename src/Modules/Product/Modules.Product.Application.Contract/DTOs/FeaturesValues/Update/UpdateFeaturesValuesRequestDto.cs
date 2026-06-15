using Modules.Product.Application.Contract.DTOs.FeaturesValuess.Create;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Product.Application.Contract.DTOs.FeaturesValuess.Update;

public class UpdateFeaturesValuesRequestDto: CreateFeaturesValuesRequestDto
{
    public long Id { get; set; }
}
