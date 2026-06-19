using Modules.Product.Application.Contract.Resources;
using Modules.Product.Application.Contract.Resources.Colors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Modules.Product.Application.Contract.DTOs.Colors.Create;

public class CreateColorRequestDto
{
    /// <summary>
    /// نام رنگ
    /// </summary>
    [Display(
        Name = nameof(ColorFieldNames.ColorName),
        ResourceType = typeof(ColorFieldNames)
    )]
    public string ColorName { get; set; } = null!;

    /// <summary>
    /// کد رنگ (Hex)
    /// </summary>
    [Display(
        Name = nameof(ColorFieldNames.ColorCode),
        ResourceType = typeof(ColorFieldNames)
    )]
    public string ColorCode { get; set; } = null!;
}
