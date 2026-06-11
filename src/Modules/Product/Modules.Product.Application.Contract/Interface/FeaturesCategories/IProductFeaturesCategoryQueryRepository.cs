using Modules.Product.Domain.Entities.FeaturesCategories;
using SharedKernel.Interface.Repositories;


namespace Modules.Product.Application.Contract.Interface.FeaturesCategories;

public interface IProductFeaturesCategoryQueryRepository : IQueryRepository<FeaturesCategory>
{
}
