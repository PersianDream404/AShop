using Modules.Product.Domain.Entities.Colors;
using Modules.Product.Domain.Entities.Discounts;
using SharedKernel.Base;

namespace Modules.Product.Domain.Entities.Products;

/// <summary>
/// محصول قابل نمایش و فروش در سیستم
/// </summary>
public class Product : BaseEntityIdentity
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

    ///// <summary>
    ///// وضعیت فعال یا غیرفعال بودن محصول
    ///// </summary>
    //public bool? IsActive { get; set; }

    /// <summary>
    /// تصویر اصلی محصول
    /// </summary>
    public string? Image { get; set; }

    /// <summary>
    /// تعداد بازدید محصول
    /// </summary>
    public int? ViewCount { get; set; }

    ///// <summary>
    ///// تعداد فروش محصول
    ///// </summary>
    //public int? SellCount { get; set; }

    /// <summary>
    /// دسته‌بندی‌های انتخاب شده محصول
    /// </summary>
    public ICollection<ProductSelectedCategory> ProductSelectedCategories { get; set; }
        = new List<ProductSelectedCategory>();



    /// <summary>
    /// تصاویر گالری محصول
    /// </summary>
    public ICollection<ProductGallery> ProductGalleries { get; set; }
        = new List<ProductGallery>();

    /// <summary>
    /// ویژگی‌های محصول
    /// </summary>
    public ICollection<ProductSelectedFeatures> ProductFeatures { get; set; }
        = new List<ProductSelectedFeatures>();

    /// <summary>
    /// تخفیف‌های محصول
    /// </summary>
    public ICollection<ProductDiscountUse> ProductDiscounts { get; set; }
        = new List<ProductDiscountUse>();

    /// <summary>
    /// رنگ‌های محصول
    /// </summary>
    public ICollection<ProductSelectedColors> ProductSelectedColors { get; set; }
        = new List<ProductSelectedColors>();

    public ICollection<ProductSelectedBrands> ProductSelectedBrands { get; set; }
    = new List<ProductSelectedBrands>();

    public ICollection<ProductComment> ProductComments { get; set; }
    = new List<ProductComment>();
}

