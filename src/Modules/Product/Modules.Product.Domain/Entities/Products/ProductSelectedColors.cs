using Modules.Product.Domain.Entities.Colors;
using SharedKernel.Base;

namespace Modules.Product.Domain.Entities.Products;

/// <summary>
/// ارتباط محصول و برند
/// </summary>
public class ProductSelectedColors : BaseEntityIdentity
{
    /// <summary>
    /// شناسه محصول
    /// </summary>
    public long ProductId { get; set; }

    /// <summary>
    /// شناسه رنگ
    /// </summary>
    public long ProductColorId { get; set; }

    /// <summary>
    /// محصول
    /// </summary>
    public Product Product { get; set; } = null!;

    /// <summary>
    /// رنگ
    /// </summary>
    public ProductColor ProductColor { get; set; } = null!;
}

