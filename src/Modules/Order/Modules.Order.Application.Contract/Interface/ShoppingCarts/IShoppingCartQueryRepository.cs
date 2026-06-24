using Modules.Order.Domain.Entities;
using SharedKernel.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Order.Application.Contract.Interface.ShoppingCarts;

public interface IShoppingCartQueryRepository : IQueryRepository<ShoppingCart>
{
    Task<ShoppingCart> GetBySessionIdAsync(long sessionId, CancellationToken cancellationToken = default);
    Task<ShoppingCart> GetByUserIdAsync(long userId, CancellationToken cancellationToken = default);
}