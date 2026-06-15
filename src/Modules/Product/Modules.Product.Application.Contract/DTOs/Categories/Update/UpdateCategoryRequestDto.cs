using Modules.Product.Application.Contract.DTOs.Categorys.Create;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Product.Application.Contract.DTOs.Categorys.Update;

public class UpdateCategoryRequestDto: CreateCategoryRequestDto
{
    public long Id { get; set; }
}
