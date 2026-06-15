using Framwork.PagedList;

namespace Modules.Product.Application.Contract.DTOs.Categorys.GetAll;

public class GetSelectListCategoryRequestDto : PagedParamData
{
    public string? Q { get; set; }
}
public class GetSelectListCategoryResponseDto
{

    public long Id { get; set; }
    /// <summary>
    /// نام 
    /// </summary>
    public string Title { get; set; } = null!;


    public bool Status { get; set; }

}