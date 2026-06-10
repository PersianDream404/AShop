using Modules.Product.Domain.Entities.Colors;
using SharedKernel.Interface.Repositories;


namespace Modules.Product.Domain.Interface.Colors;

public interface IProductColorCommandRepository : ICommandRepository<ProductColor>
{
}
