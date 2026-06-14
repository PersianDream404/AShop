using Framwork.PagedList;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Product.Application.Contract.DTOs.FeaturesCategorys.GetAll;


public class GetByIdFeaturesCategoryResponseDto
{

    public long Id { get; set; }
    /// <summary>
    /// نام 
    /// </summary>
    public string Title { get; set; } = null!;



 
    public bool Status { get; set; }
}
