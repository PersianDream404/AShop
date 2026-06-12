using Framwork.PagedList;

namespace Modules.Product.Application.Contract.DTOs.Brands.GetAll;

public class GetSelectListBrandRequestDto : PagedParamData
{
    public string? Q { get; set; }
}
public class GetSelectListBrandResponseDto
{

    public long Id { get; set; }

    /// <summary>
    /// عنوان برند
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// تصویر برند
    /// </summary>
    public string? Image { get; set; }


    /// <summary>
    /// آیکون برند
    /// </summary>
    public string? Icon { get; set; }


}