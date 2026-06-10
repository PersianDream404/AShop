using Modules.Product.Domain.Entities.Brands;
using SharedKernel.Base;

namespace Modules.Product.Domain.Entities.Products;

/// <summary>
/// ارتباط محصول و برند
/// </summary>
public class ProductSelectedBrands : BaseEntityIdentity
{
    /// <summary>
    /// شناسه محصول
    /// </summary>
    public long ProductId { get; set; }

    /// <summary>
    /// شناسه برند
    /// </summary>
    public long ProductBrandId { get; set; }

    /// <summary>
    /// محصول
    /// </summary>
    public Product Product { get; set; } = null!;

    /// <summary>
    /// برند
    /// </summary>
    public ProductBrand ProductBrand { get; set; } = null!;
}

