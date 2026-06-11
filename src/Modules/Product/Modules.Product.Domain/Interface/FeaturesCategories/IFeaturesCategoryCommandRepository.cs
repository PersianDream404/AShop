using Modules.Product.Domain.Entities.FeaturesCategories;
using SharedKernel.Interface.Repositories;


namespace Modules.Product.Domain.Interface.FeaturesCategories;

public interface IFeaturesCategoryCommandRepository : ICommandRepository<FeaturesCategory>
{
}
