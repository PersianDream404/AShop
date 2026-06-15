using Framwork.PagedList;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Product.Application.Contract.DTOs.FeaturesValuess.GetAll;

public class GetAllFeaturesValuesRequestDto : PagedParamData
{
    public string? Q { get; set; }
}
public class GetAllFeaturesValuesResponseDto
{

    public long Id { get; set; }
    /// <summary>
    /// شناسه دسته‌بندی ویژگی (مثلاً: سخت‌افزار، نمایشگر)
    /// </summary>
    public long? ProductFeaturesCategoryId { get; set; }
    public string? ProductFeaturesCategoryTitle { get; set; }

    /// <summary>
    /// عنوان ویژگی (مثلاً: RAM)
    /// </summary>
    public long? ProductFeaturesId { get; set; }
    public string? ProductFeaturesTitle { get; set; }

    /// <summary>
    /// مقدار ویژگی (مثلاً: 16GB)
    /// </summary>
    public string FeatureValue { get; set; } = null!;

    public bool Status { get; set; }
}
