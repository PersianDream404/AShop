using Modules.Order.Domain.Enums;

namespace Modules.Order.Application.Contract.DTOs;

public class OrderDto
{
    public long Id { get; set; }
    public long ShoppingCartId { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; }
    public decimal TaxAmount { get; set; }
    public bool TermsAccepted { get; set; }
    public string? ShippingAddress { get; set; }
    public string? MobileNumber { get; set; }
    public string? TrackingNumber { get; set; }
    public long DisplayId { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<OrderItemDto> OrderItems { get; set; } = [];
}

public class CreateOrderRequestDto
{
    public long ShoppingCartId { get; set; }
    public string? ShippingAddress { get; set; }
    public string? MobileNumber { get; set; }
    public string? TrackingNumber { get; set; }
}

public class UpdateOrderStatusRequestDto
{
    public long OrderId { get; set; }
    public OrderStatus NewStatus { get; set; }
}

public class UpdateTrackingNumberRequestDto
{
    public long OrderId { get; set; }
    public string TrackingNumber { get; set; }
}
