using Modules.Product.Domain.Entities.Categories;
using SharedKernel.Interface.Repositories;


namespace Modules.Product.Application.Contract.Interface.Categories;

public interface ICategoryQueryRepository : IQueryRepository<Category>
{
}
