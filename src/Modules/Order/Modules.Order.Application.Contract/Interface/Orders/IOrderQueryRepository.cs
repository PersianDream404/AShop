using Modules.Order.Domain.Entities;
using SharedKernel.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Order.Application.Contract.Interface.Orders;

public interface IOrderQueryRepository : IQueryRepository<OrderEntity>
{

    Task<IEnumerable<OrderEntity>> GetByUserIdAsync(long userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<OrderEntity>> GetBySessionIdAsync(long sessionId, CancellationToken cancellationToken = default);

}
public interface IOrderItemQueryRepository : IQueryRepository<OrderItem>
{


}
