using Infrastructure.Repositories;
using Modules.Product.Persistence.Context;
using Modules.Product.Domain.Interface;
using Modules.Product.Domain.Entities.Features;
namespace Modules.Product.Persistence.Repositories.FeaturesValuess;


public class FeaturesValuesCommandRepository
    : CommandRepository<FeaturesValues>, IFeaturesValuesCommandRepository
{
    public FeaturesValuesCommandRepository(ProductWriteDbContext context) : base(context)
    {
    }

 
}
