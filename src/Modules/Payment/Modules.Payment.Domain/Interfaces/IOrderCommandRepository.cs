using Ardalis.Result;
using Modules.Order.Domain.Entities;
using SharedKernel.Interface.Repositories;

namespace Modules.Order.Domain.Interfaces;

public interface IOrderCommandRepository: ICommandRepository<OrderEntity>
{
    Task UpdateOrderAsync(long oldCartId, long newCartId, CancellationToken cancellationToken = default);
    Task<Result<bool>> MergePendingOrderAsync(
       long targetCartId,
       long sourceCartId,
       CancellationToken cancellationToken);

}

