using Modules.Product.Application.Contract.DTOs.FeaturesValuess.Create;
using Modules.Product.Application.Contract.Resources.FeaturesValues;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Modules.Product.Application.Contract.DTOs.FeaturesValuess.Toggle;

public class ToggleFeaturesValuesRequestDto
{
    [Display(
        Name = nameof(FeaturesValuesFieldNames.Id),
        ResourceType = typeof(FeaturesValuesFieldNames)
    )]
    public long Id { get; set; }
}
