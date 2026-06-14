using Infrastructure.Repositories;
using Modules.Product.Persistence.Context;
using Modules.Product.Domain.Entities.FeaturesCategories;
using Modules.Product.Domain.Interface.FeaturesCategories;
namespace Modules.Product.Persistence.Repositories.FeaturesCategorys;


public class FeaturesCategoryCommandRepository
    : CommandRepository<FeaturesCategory>, IFeaturesCategoryCommandRepository
{
    public FeaturesCategoryCommandRepository(ProductWriteDbContext context) : base(context)
    {
    }

 
}
