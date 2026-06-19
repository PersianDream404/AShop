using Modules.Product.Application.Contract.DTOs.Categorys.Create;
using Modules.Product.Application.Contract.Resources;
using Modules.Product.Application.Contract.Resources.Categories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Modules.Product.Application.Contract.DTOs.Categorys.Update;

public class UpdateCategoryRequestDto: CreateCategoryRequestDto
{

    [Display(
        Name = nameof(CategoryFieldNames.Id),
        ResourceType = typeof(CategoryFieldNames)
    )]
    public long Id { get; set; }
}
