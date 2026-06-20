
using SharedKernel.Base;

namespace Modules.Product.Application.Contract.DTOs.Products.Create;

public class UpdateProductRequestDto: CreateProductRequestDto
{
    public int Id { get; set; }
}
