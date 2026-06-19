using Modules.Product.Application.Contract.Resources;
using Modules.Product.Application.Contract.Resources.Brands;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    [Display(
        Name = nameof(BrandFieldNames.Title),
        ResourceType = typeof(BrandFieldNames)
    )]
    public string Title { get; set; } = null!;

    /// <summary>
    /// تصویر برند
    /// </summary>
    [Display(
        Name = nameof(BrandFieldNames.Image),
        ResourceType = typeof(BrandFieldNames)
    )]
    public string? Image { get; set; }

    /// <summary>
    /// نام مورد استفاده در آدرس URL
    /// </summary>
    [Display(
        Name = nameof(BrandFieldNames.UrlName),
        ResourceType = typeof(BrandFieldNames)
    )]
    public string UrlName { get; set; } = null!;

    /// <summary>
    /// آیکون برند
    /// </summary>
    [Display(
        Name = nameof(BrandFieldNames.Icon),
        ResourceType = typeof(BrandFieldNames)
    )]
    public string? Icon { get; set; }
}
