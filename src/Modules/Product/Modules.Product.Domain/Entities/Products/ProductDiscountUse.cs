using Modules.Product.Domain.Entities.Discounts;
using SharedKernel.Base;

namespace Modules.Product.Domain.Entities.Products;

/// <summary>
/// ارتباط محصول با تخفیف‌ها (استفاده از تخفیف برای محصول)
/// </summary>
public class ProductDiscountUse : BaseEntityIdentity
{
    /// <summary>
    /// شناسه محصول
    /// </summary>
    public long ProductId { get; set; }

    /// <summary>
    /// شناسه تخفیف
    /// </summary>
    public long ProductDiscountId { get; set; }

    /// <summary>
    /// تخفیف مرتبط
    /// </summary>
    public ProductDiscount ProductDiscount { get; set; } = null!;

    /// <summary>
    /// محصول مرتبط
    /// </summary>
    public Product Product { get; set; } = null!;
}

