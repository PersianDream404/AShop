using Modules.Order.Application.Contract.DTOs;
using Modules.Order.Domain.Entities;
using SharedKernel.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Order.Application.Contract.Interface.Orders;

public interface IOrderQueryRepository : IQueryRepository<OrderEntity>
{

    Task<OrderEntity?> GetByCartIdAsync(long cartId, CancellationToken cancellationToken = default);
    Task<IEnumerable<OrderEntity>> GetByUserIdAsync(long userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<OrderEntity>> GetBySessionIdAsync(Guid sessionId, CancellationToken cancellationToken = default);
    Task<IEnumerable<OrderDto>> GetAllProjectedAsync(CancellationToken cancellationToken = default);
    Task<OrderDto?> GetByIdProjectedAsync(long id, CancellationToken cancellationToken = default);
    Task<GetPaymentSummaryOrderDto?> GetPaymentSummaryByIdProjectedAsync(long id, CancellationToken cancellationToken = default);
    Task<OrderDto?> GetBySessionIdProjectedAsync(Guid id, CancellationToken cancellationToken = default);
    Task<OrderDto?> GetByUserIdProjectedAsync(long id, CancellationToken cancellationToken = default);

    Task<decimal> GetTotalAmountByItemsAsync(long orderId);


    #region Transations
    Task<OrderTransaction?> GetOrderTransactionByIdAsync(long TransactionId, CancellationToken cancellationToken = default);

    #endregion

}
public interface IOrderItemQueryRepository : IQueryRepository<OrderItem>
{
    Task<OrderItem?> GetAsync(long ProductId, long OrderId, CancellationToken cancellationToken = default);

}
