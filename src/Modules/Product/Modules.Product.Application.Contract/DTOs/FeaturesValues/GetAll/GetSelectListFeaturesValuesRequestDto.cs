using Framwork.PagedList;

namespace Modules.Product.Application.Contract.DTOs.FeaturesValuess.GetAll;

public class GetSelectListFeaturesValuesRequestDto : PagedParamData
{
    public string? Q { get; set; }
}
public class GetSelectListFeaturesValuesResponseDto
{

    public long Id { get; set; }
    /// <summary>
    /// نام 
    /// </summary>
    public string Title { get; set; } = null!;


    public bool Status { get; set; }

}