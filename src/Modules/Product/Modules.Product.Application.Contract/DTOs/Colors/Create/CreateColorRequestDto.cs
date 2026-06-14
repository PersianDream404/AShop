using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Product.Application.Contract.DTOs.Colors.Create;

public class CreateColorRequestDto
{

    /// <summary>
    /// نام رنگ
    /// </summary>
    public string ColorName { get; set; } = null!;

    /// <summary>
    /// کد رنگ (Hex)
    /// </summary>
    public string ColorCode { get; set; } = null!;




}
