using Framwork.PagedList;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Product.Application.Contract.DTOs.Brands.GetAll;

public class GetAllBrandRequestDto : PagedParamData
{
    public string? Q { get; set; }
}
public class GetAllBrandResponseDto
{

    public long Id { get; set; }
    /// <summary>
    /// شناسه برند والد
    /// </summary>
    public long? ParentId { get; set; }

    /// <summary>
    /// عنوان برند
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// تصویر برند
    /// </summary>
    public string? Image { get; set; }

    /// <summary>
    /// نام مورد استفاده در آدرس URL
    /// </summary>
    public string UrlName { get; set; } = null!;

    /// <summary>
    /// آیکون برند
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// وضعیت فعال بودن برند
    /// </summary>
    public bool IsActive { get; set; }
}
