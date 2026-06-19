using Modules.Product.Application.Contract.Resources;
using Modules.Product.Application.Contract.Resources.Categories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Modules.Product.Application.Contract.DTOs.Categorys.Create;

public class CreateCategoryRequestDto
{
    /// <summary>
    /// شناسه دسته‌بندی والد
    /// </summary>
    public long? ParentId { get; set; }

    /// <summary>
    /// عنوان دسته‌بندی
    /// </summary>
    [Display(
        Name = nameof(CategoryFieldNames.Title),
        ResourceType = typeof(CategoryFieldNames)
    )]
    public string Title { get; set; } = null!;

    /// <summary>
    /// تصویر دسته‌بندی
    /// </summary>
    [Display(
        Name = nameof(CategoryFieldNames.Image),
        ResourceType = typeof(CategoryFieldNames)
    )]
    public string? Image { get; set; }

    /// <summary>
    /// نام مورد استفاده در آدرس URL
    /// </summary>
    [Display(
        Name = nameof(CategoryFieldNames.UrlName),
        ResourceType = typeof(CategoryFieldNames)
    )]
    public string UrlName { get; set; } = null!;

    /// <summary>
    /// آیکون دسته‌بندی
    /// </summary>
    [Display(
        Name = nameof(CategoryFieldNames.Icon),
        ResourceType = typeof(CategoryFieldNames)
    )]
    public string? Icon { get; set; }





}
