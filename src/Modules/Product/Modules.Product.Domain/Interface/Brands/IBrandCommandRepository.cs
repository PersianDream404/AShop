using Modules.Product.Domain.Entities.Brands;
using SharedKernel.Interface.Repositories;


namespace Modules.Product.Domain.Interface.Brands;

public interface IBrandCommandRepository : ICommandRepository<Brand>
{
}
