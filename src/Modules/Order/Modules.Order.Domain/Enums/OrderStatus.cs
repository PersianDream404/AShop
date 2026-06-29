namespace Modules.Order.Domain.Enums;


public enum OrderStatus
{
    PendingPayment = 1,
    Paid = 2,
    Cancelled = 3,
    Processing = 4,
    Shipped = 5,
    Completed = 6
}
public enum OrderTransactionStatus
{
    Pending = 1,
    Succeeded = 2,
    Failed = 3
}