using Modules.Product.Domain.Entities.Products;
using SharedKernel.Base;
using System.ComponentModel.DataAnnotations;

namespace Modules.Product.Domain.Entities.Discounts;

/// <summary>
/// تعریف تخفیف
/// </summary>
public class ProductDiscount : BaseEntityIdentity
{
    /// <summary>
    /// درصد تخفیف
    /// </summary>
    [Range(0, 100)]
    public int Percentage { get; set; }

    /// <summary>
    /// تاریخ انقضا
    /// </summary>
    public DateTime ExpireDate { get; set; }

    /// <summary>
    /// حداکثر تعداد استفاده (اختیاری)
    /// </summary>
    public int? MaxUsageCount { get; set; }

    /// <summary>
    /// تعداد استفاده شده
    /// </summary>
    public int UsedCount { get; set; }

    /// <summary>
    /// تخفیف‌های اعمال شده روی محصولات
    /// </summary>
    public ICollection<ProductDiscountUse> ProductDiscountUses { get; set; }
        = new List<ProductDiscountUse>();
}

