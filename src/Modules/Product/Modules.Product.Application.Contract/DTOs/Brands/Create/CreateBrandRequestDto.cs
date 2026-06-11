using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Product.Application.Contract.DTOs.Brands.Create;

public class CreateBrandRequestDto
{

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


}
