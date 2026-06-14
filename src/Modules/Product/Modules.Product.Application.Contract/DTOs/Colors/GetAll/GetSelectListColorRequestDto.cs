using Framwork.PagedList;

namespace Modules.Product.Application.Contract.DTOs.Colors.GetAll;

public class GetSelectListColorRequestDto : PagedParamData
{
    public string? Q { get; set; }
}
public class GetSelectListColorResponseDto
{

    public long Id { get; set; }
    /// <summary>
    /// نام رنگ
    /// </summary>
    public string ColorName { get; set; } = null!;

    /// <summary>
    /// کد رنگ (Hex)
    /// </summary>
    public string ColorCode { get; set; } = null!;


    public bool Status { get; set; }

}