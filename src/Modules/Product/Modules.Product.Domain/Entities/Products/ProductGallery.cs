using SharedKernel.Base;

namespace Modules.Product.Domain.Entities.Products;

/// <summary>
/// تصویر گالری محصول
/// </summary>
public class ProductGallery : BaseEntityIdentity
{
    /// <summary>
    /// شناسه محصول
    /// </summary>
    public long ProductId { get; set; }

    /// <summary>
    /// اولویت نمایش تصویر در گالری
    /// </summary>
    public int DisplayPriority { get; set; }

    /// <summary>
    /// نام فایل تصویر
    /// </summary>
    public string ImageName { get; set; } = null!;

    /// <summary>
    /// محصول مرتبط
    /// </summary>
    public Product Product { get; set; } = null!;
}

