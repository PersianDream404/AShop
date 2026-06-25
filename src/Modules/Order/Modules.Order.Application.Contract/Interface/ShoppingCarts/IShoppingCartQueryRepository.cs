using Modules.Order.Application.Contract.DTOs;
using Modules.Order.Domain.Entities;
using SharedKernel.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Order.Application.Contract.Interface.ShoppingCarts;

public interface IShoppingCartQueryRepository : IQueryRepository<ShoppingCart>
{
    Task<ShoppingCartDto?> GetBySessionIdProjectedAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> AnyBySessionIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ShoppingCartDto?> GetByUserIdProjectedAsync(long id, CancellationToken cancellationToken = default);
    Task<ShoppingCart?> GetByNotIdAsync(long UserId, long CartId,CancellationToken cancellationToken = default);
}