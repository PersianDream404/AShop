
using Modules.Product.Domain.Entities.Features;
using Modules.Product.Domain.Entities.Products;
using SharedKernel.Interface.Repositories;


namespace Modules.Product.Domain.Interface;

public interface IProductFeaturesCommandRepository : ICommandRepository<ProductFeatures>
{
}
