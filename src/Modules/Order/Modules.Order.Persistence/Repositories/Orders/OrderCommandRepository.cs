using Ardalis.Result;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Modules.Order.Domain.Entities;
using Modules.Order.Domain.Enums;
using Modules.Order.Domain.Interfaces;
using Modules.Order.Persistence.Context;

namespace Modules.Order.Persistence.Repositories.Orders;

public class OrderCommandRepository : CommandRepository<OrderEntity>, IOrderCommandRepository
{
    private readonly OrderWriteDbContext _context;

    public OrderCommandRepository(OrderWriteDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task UpdateOrderAsync(long oldCartId, long newCartId,CancellationToken cancellationToken=default)
    {
        var orders = await _context.Orders
            .Where(x => x.ShoppingCartId == oldCartId)
            .ToListAsync(cancellationToken);

        foreach (var order in orders)
        {
            order.ChangeCart(newCartId);
        }


    }
    public async Task<Result<bool>> MergePendingOrderAsync(
       long targetCartId,
       long sourceCartId,
       CancellationToken cancellationToken)
    {
        if (targetCartId == sourceCartId)
            return Result.Success(true);

        var orders = await _context.Orders
            .Where(x =>
                (x.ShoppingCartId == targetCartId || x.ShoppingCartId == sourceCartId) &&
                x.OrderStatus == OrderStatus.PendingPayment)
            .ToListAsync(cancellationToken);

        var targetOrder = orders.FirstOrDefault(x => x.ShoppingCartId == targetCartId);
        var sourceOrder = orders.FirstOrDefault(x => x.ShoppingCartId == sourceCartId);

        if (sourceOrder is null)
            return Result.Success(true);

        if (targetOrder is null)
        {
            sourceOrder.ChangeCart(targetCartId);
            _context.Orders.Update(sourceOrder);
            return Result.Success(true);
        }

        var items = await _context.OrderItems
            .Where(x => x.OrderId == targetOrder.Id || x.OrderId == sourceOrder.Id)
            .ToListAsync(cancellationToken);

        var targetItemsByProductId = items
            .Where(x => x.OrderId == targetOrder.Id)
            .GroupBy(x => x.ProductId)
            .ToDictionary(x => x.Key, x => x.First());

        var sourceItems = items
            .Where(x => x.OrderId == sourceOrder.Id)
            .ToList();

        foreach (var sourceItem in sourceItems)
        {
            if (targetItemsByProductId.TryGetValue(sourceItem.ProductId, out var targetItem))
            {
                var updateResult = targetItem.IncreaseQuantity(sourceItem.Quantity);

                if (!updateResult.IsSuccess)
                    return Result.Invalid(updateResult.ValidationErrors);

                _context.OrderItems.Remove(sourceItem);
                continue;
            }

            sourceItem.ChangeOrder(targetOrder.Id);
            _context.OrderItems.Update(sourceItem);
        }

        _context.Orders.Remove(sourceOrder);

        return Result.Success(true);
    }

}
