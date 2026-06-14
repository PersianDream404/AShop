using Infrastructure.Repositories;
using Modules.Product.Persistence.Context;
using Modules.Product.Domain.Interface;
using Modules.Product.Domain.Entities.Features;
namespace Modules.Product.Persistence.Repositories.ProductFeaturess;


public class ProductFeaturesCommandRepository
    : CommandRepository<ProductFeatures>, IProductFeaturesCommandRepository
{
    public ProductFeaturesCommandRepository(ProductWriteDbContext context) : base(context)
    {
    }

 
}
