using Modules.Product.Application.Contract.DTOs.Products.Create;

namespace Modules.Product.Application.Contract.DTOs.Products.Get;

public class GetByIdProductResponseDto
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
    /// 
    public IEnumerable<GetByIdProductGalleryRequestDto> ProductGalleries { get; set; } = [];
    public IEnumerable<GetByIdProductSelectedFeaturesRequestDto> ProductFeatures { get; set; } = [];
    public IEnumerable<GetByIdProductColorsSelectListResponseDto> Colors { get; set; } = [];
    public IEnumerable<GetByIdProductCategoriesSelectListResponseDto> Categories { get; set; } = [];
    public IEnumerable<GetByIdProductBrandsSelectListResponseDto> Brands { get; set; } = [];
}
public record GetByIdProductColorsSelectListResponseDto(long Id,string Name);
public record GetByIdProductCategoriesSelectListResponseDto(long Id,string Name);
public record GetByIdProductBrandsSelectListResponseDto(long Id,string Name);


public record GetByIdProductSelectedFeaturesRequestDto(long Id, long? ProductFeaturesCategoryId, long? ProductFeaturesId, string FeatureValue);

public record GetByIdProductGalleryRequestDto(long Id, int DisplayPriority, string ImageName);
