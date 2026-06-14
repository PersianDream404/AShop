using Modules.Product.Application.Contract.DTOs.Colors.Create;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Product.Application.Contract.DTOs.Colors.Update;

public class UpdateColorRequestDto: CreateColorRequestDto
{
    public long Id { get; set; }
}
