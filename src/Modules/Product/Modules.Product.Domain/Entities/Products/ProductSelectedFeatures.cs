using Modules.Product.Domain.Entities.Features;
using Modules.Product.Domain.Entities.FeaturesCategories;
using SharedKernel.Base;

namespace Modules.Product.Domain.Entities.Products;

/// <summary>
/// ویژگی‌های محصول
/// </summary>
public class ProductSelectedFeatures : BaseEntityIdentity
{
    /// <summary>
    /// شناسه محصول
    /// </summary>
    public long ProductId { get; set; }

    /// <summary>
    /// شناسه دسته‌بندی ویژگی (مثلاً: سخت‌افزار، نمایشگر)
    /// </summary>
    public long? ProductFeaturesCategoryId { get; set; }

    /// <summary>
    /// عنوان ویژگی (مثلاً: RAM)
    /// </summary>
    public long? ProductFeaturesId { get; set; }

    /// <summary>
    /// مقدار ویژگی (مثلاً: 16GB)
    /// </summary>
    public string FeatureValue { get; set; } = null!;

    /// <summary>
    /// دسته‌بندی ویژگی
    /// </summary>
    public ProductFeaturesCategory? ProductFeaturesCategory { get; set; }
    public ProductFeatures? ProductFeatures { get; set; }

    /// <summary>
    /// محصول مرتبط
    /// </summary>
    public Product Product { get; set; } = null!;
}

