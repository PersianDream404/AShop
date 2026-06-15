using Modules.Product.Domain.Entities.Features;
using Modules.Product.Domain.Entities.Products;
using SharedKernel.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Product.Domain.Entities.FeaturesCategories;

/// <summary>
/// گروه ویژگی‌های محصول
/// </summary>
public class FeaturesCategory : BaseEntityIdentity
{
    /// <summary>
    /// عنوان گروه ویژگی
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// ویژگی‌های مرتبط با این گروه
    /// </summary>
    public ICollection<ProductSelectedFeatures> ProductFeatures { get; set; }
        = new List<ProductSelectedFeatures>();

    public ICollection<FeaturesValues> FeaturesValues { get; set; }
        = [];
}

