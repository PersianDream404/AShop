using Modules.Order.Domain.Enums;
using Shared.Contract.Enums.Payments;
using SharedKernel.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Modules.Order.Domain.Entities;



public sealed class OrderTransaction:BaseEntityIdentity
{
    private OrderTransaction() { }

    public OrderTransaction(long orderId, decimal amount)
    {
        OrderId = orderId;
        Amount = amount;
        OrderStatus = OrderTransactionStatus.Pending;
        CreatedAt = DateTime.UtcNow;
    }


    public long OrderId { get; private set; }
    public OrderEntity Order { get; private set; }

    public decimal Amount { get; private set; }

    public OrderTransactionStatus OrderStatus { get; private set; }

    public string? GatewayTransactionCode { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime? PaidAt { get; private set; }

    public DateTime? FailedAt { get; private set; }

    public void MarkSucceeded(string gatewayTransactionCode)
    {
        if (OrderStatus == OrderTransactionStatus.Succeeded)
            return;

        OrderStatus = OrderTransactionStatus.Succeeded;
        GatewayTransactionCode = gatewayTransactionCode;
        PaidAt = DateTime.Now;
    }

    public void MarkFailed()
    {
        if (OrderStatus == OrderTransactionStatus.Succeeded)
            return;

        OrderStatus = OrderTransactionStatus.Failed;
        FailedAt = DateTime.UtcNow;
    }
}

