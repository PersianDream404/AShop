using SharedKernel.Base;

namespace Modules.Product.Domain.Entities.Products;

/// <summary>
/// نظر کاربران درباره محصول
/// </summary>
public class ProductComment : BaseEntityIdentity
{
    /// <summary>
    /// شناسه محصول
    /// </summary>
    public long ProductId { get; set; }

    /// <summary>
    /// ایمیل کاربر (اختیاری)
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// نام و نام خانوادگی کاربر
    /// </summary>
    public string FullName { get; set; } = null!;

    /// <summary>
    /// متن نظر
    /// </summary>
    public string Message { get; set; } = null!;

    /// <summary>
    /// نقاط قوت محصول
    /// </summary>
    public string? StrongPoint { get; set; }

    /// <summary>
    /// نقاط ضعف محصول
    /// </summary>
    public string? WeakPoint { get; set; }

    /// <summary>
    /// محصول مرتبط
    /// </summary>
    public Product Product { get; set; } = null!;
}

