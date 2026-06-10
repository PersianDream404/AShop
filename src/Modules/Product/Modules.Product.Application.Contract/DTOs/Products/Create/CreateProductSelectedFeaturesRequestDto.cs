namespace Modules.Product.Application.Contract.DTOs.Products.Create;

/// <summary>
/// ویژگی‌های محصول
/// </summary>
public class CreateProductSelectedFeaturesRequestDto
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
}
