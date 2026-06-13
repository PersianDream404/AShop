using Modules.Product.Domain.Entities.Products;
using SharedKernel.Base;

namespace Modules.Product.Domain.Entities.Features;

/// <summary>
/// ویژگی‌های محصول
/// </summary>
public class ProductFeatures : BaseEntityIdentity
{

    /// <summary>
    /// عنوان ویژگی (مثلاً: RAM)
    /// </summary>
    public string Title { get; set; } = null!;


    public ICollection<ProductSelectedFeatures> ProductSelectedFeatures { get; set; } = [];
}

