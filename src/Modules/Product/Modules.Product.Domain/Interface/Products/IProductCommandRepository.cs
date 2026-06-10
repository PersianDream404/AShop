using SharedKernel.Interface.Repositories;


namespace Modules.Product.Domain.Interface.Products;

public interface IProductCommandRepository : ICommandRepository<Entities.Products.Product>
{
}
