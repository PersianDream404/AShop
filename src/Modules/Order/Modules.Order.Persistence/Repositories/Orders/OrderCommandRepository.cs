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

    public async Task<OrderEntity?> GetByIdForUserAsync(
        long orderId,
        long userId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Orders
            .Include(x => x.ShoppingCart)
            .FirstOrDefaultAsync(
                x => x.Id == orderId &&
                     x.ShoppingCart.UserId == userId &&
                     x.OrderStatus == OrderStatus.PendingPayment,
                cancellationToken);
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
            .Where(x => x.ShoppingCartId == targetCartId || x.ShoppingCartId == sourceCartId)
            .OrderBy(x => x.CreatedAt)
            .ThenBy(x => x.Id)
            .ToListAsync(cancellationToken);

        var targetOrder = orders.FirstOrDefault(x =>
            x.ShoppingCartId == targetCartId &&
            x.OrderStatus == OrderStatus.PendingPayment);
        var pendingSourceOrders = orders
            .Where(x =>
                x.ShoppingCartId == sourceCartId &&
                x.OrderStatus == OrderStatus.PendingPayment)
            .ToList();

        targetOrder ??= pendingSourceOrders.FirstOrDefault();

        if (targetOrder is not null)
        {
            var pendingOrdersToMerge = orders
                .Where(x =>
                    x.OrderStatus == OrderStatus.PendingPayment &&
                    x.Id != targetOrder.Id)
                .ToList();

            if (pendingOrdersToMerge.Count > 0)
            {
                var sourceOrderIds = pendingOrdersToMerge.Select(x => x.Id).ToHashSet();
                var items = await _context.OrderItems
                    .Where(x => sourceOrderIds.Contains(x.OrderId) || x.OrderId == targetOrder.Id)
                    .ToListAsync(cancellationToken);

                var targetItems = items
                    .Where(x => x.OrderId == targetOrder.Id)
                    .ToList();

                foreach (var sourceItem in items.Where(x => sourceOrderIds.Contains(x.OrderId)))
                {
                    var matchingTargetItem = sourceItem.DiscountValue is null
                        ? targetItems.FirstOrDefault(x =>
                            x.ProductId == sourceItem.ProductId &&
                            x.UnitPrice == sourceItem.UnitPrice &&
                            x.DiscountValue is null)
                        : null;

                    if (matchingTargetItem is not null)
                    {
                        var increaseResult = matchingTargetItem.IncreaseQuantity(sourceItem.Quantity);
                        if (!increaseResult.IsSuccess)
                            return Result.Invalid(increaseResult.ValidationErrors);

                        _context.OrderItems.Remove(sourceItem);
                    }
                    else
                    {
                        var changeResult = sourceItem.ChangeOrder(targetOrder.Id);
                        if (!changeResult.IsSuccess)
                            return Result.Invalid(changeResult.ValidationErrors);

                        targetItems.Add(sourceItem);
                    }
                }

                foreach (var sourceOrder in pendingOrdersToMerge)
                {
                    var totalResult = targetOrder.UpdateTotalAmount(
                        targetOrder.TotalAmount + sourceOrder.TotalAmount);
                    if (!totalResult.IsSuccess)
                        return Result.Invalid(totalResult.ValidationErrors);

                    _context.Orders.Remove(sourceOrder);
                }
            }

            var changeCartResult = targetOrder.ChangeCart(targetCartId);
            if (!changeCartResult.IsSuccess)
                return Result.Invalid(changeCartResult.ValidationErrors);
        }

        foreach (var order in orders.Where(x =>
                     x.ShoppingCartId == sourceCartId &&
                     x.OrderStatus != OrderStatus.PendingPayment))
        {
            var changeCartResult = order.TransferCart(targetCartId);
            if (!changeCartResult.IsSuccess)
                return Result.Invalid(changeCartResult.ValidationErrors);
        }

        return Result.Success(true);
    }

}
