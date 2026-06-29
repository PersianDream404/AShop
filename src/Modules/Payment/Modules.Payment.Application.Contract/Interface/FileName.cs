using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Payment.Application.Contract.Interface;

using Modules.Orders.Domain.Orders;

namespace Modules.Orders.Application.Abstractions;

public interface IOrderRepository
{
    Task<OrderEntity?> GetByIdAsync(int id, CancellationToken ct);

    Task<OrderTransaction?> GetTransactionByIdAsync(int id, CancellationToken ct);

    Task SaveChangesAsync(CancellationToken ct);
}
