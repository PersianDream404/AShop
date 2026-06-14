using Modules.Product.Domain.Entities.Products;
using SharedKernel.Base;

namespace Modules.Product.Domain.Entities.Colors;

/// <summary>
/// رنگ قابل انتخاب برای محصول
/// </summary>
public class Color : BaseEntityIdentity
{


    /// <summary>
    /// نام رنگ
    /// </summary>
    public string ColorName { get; set; } = null!;

    /// <summary>
    /// کد رنگ (Hex)
    /// </summary>
    public string ColorCode { get; set; } = null!;



    /// <summary>
    /// رنگ‌های محصول
    /// </summary>
    public ICollection<ProductSelectedColors> ProductSelectedColors { get; set; }
        = new List<ProductSelectedColors>();
}

