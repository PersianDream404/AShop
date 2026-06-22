using Framwork.PagedList;

namespace Modules.Product.Application.Contract.DTOs.Products.GetAll;

public class GetSelectListProductRequestDto : PagedParamData
{
    public string? Q { get; set; }
}
public class GetSelectListProductResponseDto
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

    public bool Status { get; set; }


}