namespace Modules.Product.Application.Contract.DTOs.Products.Create;

/// <summary>
/// تصویر گالری محصول
/// </summary>
public class CreateProductGalleryRequestDto 
{


    /// <summary>
    /// اولویت نمایش تصویر در گالری
    /// </summary>
    public int DisplayPriority { get; set; }

    /// <summary>
    /// نام فایل تصویر
    /// </summary>
    public string ImageName { get; set; } = null!;
}
