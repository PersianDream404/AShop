using Framwork.PagedList;

namespace Modules.Product.Application.Contract.DTOs.FeaturesCategorys.GetAll;

public class GetSelectListFeaturesCategoryRequestDto : PagedParamData
{
    public string? Q { get; set; }
}
public class GetSelectListFeaturesCategoryResponseDto
{

    public long Id { get; set; }
    /// <summary>
    /// نام 
    /// </summary>
    public string Title { get; set; } = null!;




    public bool Status { get; set; }

}