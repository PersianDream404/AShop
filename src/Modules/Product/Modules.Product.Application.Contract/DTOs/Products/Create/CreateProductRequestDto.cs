
using SharedKernel.Base;

namespace Modules.Product.Application.Contract.DTOs.Products.Create;

public class CreateProductRequestDto
{
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
    public bool? IsActive { get; set; }

    /// <summary>
    /// تصویر اصلی محصول
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
    public List<int> CategoriesIds { get; set; } = [];
    public List<int> SelectedColorsIds { get; set; } = [];
    public List<int> DiscountsIds { get; set; } = [];
    public List<CreateProductSelectedFeaturesRequestDto> ProductFeatures { get; set; } = [];
    public List<CreateProductGalleryRequestDto> ProductGalleries { get; set; } = [];
}
