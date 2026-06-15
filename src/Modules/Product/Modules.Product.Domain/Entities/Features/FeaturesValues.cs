using Modules.Product.Domain.Entities.FeaturesCategories;
using SharedKernel.Base;

namespace Modules.Product.Domain.Entities.Features;

/// <summary>
/// ویژگی‌های محصول
/// </summary>
public class FeaturesValues : BaseEntityIdentity
{


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
    public FeaturesCategory? ProductFeaturesCategory { get; set; }
    public ProductFeatures? ProductFeatures { get; set; }

}
