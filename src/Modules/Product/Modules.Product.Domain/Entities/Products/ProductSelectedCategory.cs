using Modules.Product.Domain.Entities.Categories;
using SharedKernel.Base;

namespace Modules.Product.Domain.Entities.Products;

/// <summary>
/// ارتباط بین محصول و دسته‌بندی
/// </summary>
public class ProductSelectedCategory : BaseEntityIdentity
{
    /// <summary>
    /// شناسه محصول
    /// </summary>
    public long ProductId { get; set; }

    /// <summary>
    /// شناسه دسته‌بندی
    /// </summary>
    public long ProductCategoryId { get; set; }

    /// <summary>
    /// محصول
    /// </summary>
    public Product Product { get; set; } = null!;

    /// <summary>
    /// دسته‌بندی
    /// </summary>
    public Category ProductCategory { get; set; } = null!;
}

