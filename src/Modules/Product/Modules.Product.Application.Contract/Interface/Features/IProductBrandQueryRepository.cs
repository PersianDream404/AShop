using Modules.Product.Domain.Entities.Features;
using SharedKernel.Interface.Repositories;


namespace Modules.Product.Application.Contract.Interface.Features;

public interface IProductFeaturesQueryRepository : IQueryRepository<ProductFeatures>
{
}
