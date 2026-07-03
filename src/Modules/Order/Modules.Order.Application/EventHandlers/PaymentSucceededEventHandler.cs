namespace Modules.Order.Application.EventHandlers;

using global::Modules.Order.Application.Contract.Interface.Orders;
using global::Modules.Payment.Application.Contract.Events;
using MediatR;
using Modules.Order.Domain.Interfaces;
using Modules.Payment.Application.Contract.Events;
using SharedKernel.Interface.Repositories;

public sealed class PaymentSucceededEventHandler(
    IOrderQueryRepository orderRepository, IOrderCommandRepository orderCommandRepository,
    IOrderTransactionCommandRepository orderTransactionCommandRepository,IUnitOfWork unitOfWork
) : INotificationHandler<PaymentSucceededEvent>
{
    public async Task Handle(
        PaymentSucceededEvent notification,
        CancellationToken ct)
    {
        var transaction = await orderRepository.GetOrderTransactionByIdAsync(
            (int)notification.TrackingNumber,
            ct);

        if (transaction is null)
            return;

        transaction.MarkSucceeded(notification.GatewayTransactionCode);

        var order = await orderRepository.GetByIdAsync(transaction.OrderId, ct);

        if (order is null)
            return;

        order.MarkPaid();
        await orderTransactionCommandRepository.UpdateAsync(transaction, ct);
        
        await orderCommandRepository.UpdateAsync(order, ct);
        await unitOfWork.SaveChangesAsync(ct);
    }
}


public sealed class PaymentFailedEventHandler(
    IOrderQueryRepository orderRepository, IOrderCommandRepository orderCommandRepository,
    IOrderTransactionCommandRepository orderTransactionCommandRepository, IUnitOfWork unitOfWork
) : INotificationHandler<PaymentFailedEvent>
{
    public async Task Handle(
        PaymentFailedEvent notification,
        CancellationToken ct)
    {
        var transaction = await orderRepository.GetOrderTransactionByIdAsync(
            (int)notification.TrackingNumber,
            ct);

        if (transaction is null)
            return;

        transaction.MarkFailed();

        var order = await orderRepository.GetByIdAsync(transaction.OrderId, ct);

        order?.MarkPaymentFailed();
        await orderTransactionCommandRepository.UpdateAsync(transaction, ct);
        await orderCommandRepository.UpdateAsync(order, ct);
        await unitOfWork.SaveChangesAsync(ct);
    }
}
