using Modules.Product.Application.Contract.DTOs.Colors.Create;
using Modules.Product.Application.Contract.Resources.Colors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Modules.Product.Application.Contract.DTOs.Colors.Update;

public class UpdateColorRequestDto: CreateColorRequestDto
{
    [Display(
        Name = nameof(ColorFieldNames.Id),
        ResourceType = typeof(ColorFieldNames)
    )]
    public long Id { get; set; }
}
