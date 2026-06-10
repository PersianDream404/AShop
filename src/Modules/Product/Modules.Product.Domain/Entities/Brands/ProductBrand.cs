using Modules.Product.Domain.Entities.Products;
using SharedKernel.Base;

namespace Modules.Product.Domain.Entities.Brands;

/// <summary>
/// برند محصولات
/// </summary>
public class ProductBrand : BaseEntityIdentity
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

    /// <summary>
    /// وضعیت فعال بودن برند
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// برند والد
    /// </summary>
    public ProductBrand? Parent { get; set; }

    /// <summary>
    /// زیر برندها
    /// </summary>
    public ICollection<ProductBrand> Children { get; set; }
        = new List<ProductBrand>();

    /// <summary>
    /// محصولات مرتبط با برند
    /// </summary>
    public ICollection<ProductSelectedBrands> ProductSelectedBrands { get; set; }
        = new List<ProductSelectedBrands>();
}

