using Framwork.PagedList;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Product.Application.Contract.DTOs.Products.GetAll;

public class GetAllProductRequestDto : PagedParamData
{
    public string? Q { get; set; }
}
public class GetAllProductResponseDto
{
    public long Id { get; set; }
    /// <summary>
    /// نام محصول
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// کد محصول
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// قیمت محصول
    /// </summary>
    public int Price { get; set; }

    /// <summary>
    /// توضیحات کوتاه محصول
    /// </summary>
    public string? ShortDescription { get; set; }

    /// <summary>
    /// توضیحات کامل محصول
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// وضعیت فعال یا غیرفعال بودن محصول
    /// </summary>
    public string? Image { get; set; }

    /// <summary>
    /// تعداد بازدید محصول
    /// </summary>
    public int? ViewCount { get; set; }

    /// <summary>
    /// تعداد فروش محصول
    /// </summary>
    public int? SellCount { get; set; }

    /// <summary>
    /// دسته‌بندی‌های انتخاب شده محصول
    /// </summary>
    /// 
    public bool Status { get; set; }
}
