using SharedKernel.Base;
using System.ComponentModel.DataAnnotations;

namespace Product.Domain.Entities;

public class ProductCategory : BaseEntity
{
    #region Properties

    public long? ParentId { get; set; }

    [Display(Name = "عنوان دسته بندی")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string Title { get; set; } = string.Empty;

    [Display(Name = "تصویر دسته بندی")]
    [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string? Image { get; set; }

    [Display(Name = "عنوان در لینک URL")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string UrlName { get; set; } = string.Empty;

    [Display(Name = "آیکون")]
    [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string? Icon { get; set; }

    [Display(Name = "فعال / غیرفعال")]
    public bool IsActive { get; set; }

    #endregion

    #region Relations

    public ICollection<ProductSelectedCategory> ProductSelectedCategories { get; set; } = new List<ProductSelectedCategory>();
    public ProductCategory? Parent { get; set; }
    public ICollection<ProductCategory> Children { get; set; } = new List<ProductCategory>();

    #endregion
}
