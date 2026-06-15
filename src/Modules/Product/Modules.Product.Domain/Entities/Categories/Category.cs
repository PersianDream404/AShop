using Modules.Product.Domain.Entities.Products;
using SharedKernel.Base;

namespace Modules.Product.Domain.Entities.Categories;

/// <summary>
/// دسته‌بندی محصولات
/// </summary>
public class Category : BaseEntityIdentity
{
    /// <summary>
    /// شناسه دسته‌بندی والد
    /// </summary>
    public long? ParentId { get; set; }

    /// <summary>
    /// عنوان دسته‌بندی
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// تصویر دسته‌بندی
    /// </summary>
    public string? Image { get; set; }

    /// <summary>
    /// نام مورد استفاده در آدرس URL
    /// </summary>
    public string UrlName { get; set; } = null!;

    /// <summary>
    /// آیکون دسته‌بندی
    /// </summary>
    public string? Icon { get; set; }



    /// <summary>
    /// دسته‌بندی والد
    /// </summary>
    public Category? Parent { get; set; }

    /// <summary>
    /// زیر دسته‌ها
    /// </summary>
    public ICollection<Category> Children { get; set; }
        = new List<Category>();

    /// <summary>
    /// محصولات مرتبط با دسته‌بندی
    /// </summary>
    public ICollection<ProductSelectedCategory> ProductSelectedCategories { get; set; }
        = new List<ProductSelectedCategory>();
}

