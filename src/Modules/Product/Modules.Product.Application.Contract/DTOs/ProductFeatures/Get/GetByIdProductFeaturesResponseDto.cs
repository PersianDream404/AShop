using Framwork.PagedList;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Product.Application.Contract.DTOs.ProductFeaturess.GetAll;


public class GetByIdProductFeaturesResponseDto
{

    public long Id { get; set; }
    /// <summary>
    /// نام 
    /// </summary>
    public string Title { get; set; } = null!;



 
    public bool Status { get; set; }
}
