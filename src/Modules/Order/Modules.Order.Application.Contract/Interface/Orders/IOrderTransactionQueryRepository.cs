using Modules.Order.Domain.Entities;
using SharedKernel.Interface.Repositories;

namespace Modules.Order.Application.Contract.Interface.ShoppingCarts;

public interface IOrderTransactionQueryRepository : IQueryRepository<OrderTransaction>
{
}