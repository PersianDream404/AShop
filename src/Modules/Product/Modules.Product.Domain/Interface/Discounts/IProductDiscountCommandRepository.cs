using Modules.Product.Domain.Entities.Discounts;
using SharedKernel.Interface.Repositories;


namespace Modules.Product.Domain.Interface.Discounts;

public interface IProductDiscountCommandRepository : ICommandRepository<ProductDiscount>
{
}
