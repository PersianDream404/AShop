using Framwork.PagedList;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Product.Application.Contract.DTOs.Colors.GetAll;

public class GetAllColorRequestDto : PagedParamData
{
    public string? Q { get; set; }
}
public class GetAllColorResponseDto
{

    public long Id { get; set; }
    /// <summary>
    /// نام رنگ
    /// </summary>
    public string ColorName { get; set; } = null!;

    /// <summary>
    /// کد رنگ (Hex)
    /// </summary>
    public string ColorCode { get; set; } = null!;


    public bool Status { get; set; }
}
