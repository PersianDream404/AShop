using Framwork.PagedList;

namespace Modules.Product.Application.Contract.DTOs.ProductFeaturess.GetAll;

public class GetSelectListProductFeaturesRequestDto : PagedParamData
{
    public string? Q { get; set; }
}
public class GetSelectListProductFeaturesResponseDto
{

    public long Id { get; set; }
    /// <summary>
    /// نام 
    /// </summary>
    public string Title { get; set; } = null!;


    public bool Status { get; set; }

}