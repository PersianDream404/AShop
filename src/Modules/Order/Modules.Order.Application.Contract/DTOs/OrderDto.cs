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
    public long? ShippingAddressId { get; set; }
    public string? ShippingRecipientName { get; set; }
    public string? ShippingProvince { get; set; }
    public string? ShippingCity { get; set; }
    public string? ShippingPostalCode { get; set; }
    public string? ShippingAddress { get; set; }
    public string? MobileNumber { get; set; }
    public string? TrackingNumber { get; set; }
    public long DisplayId { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<OrderItemDto> OrderItems { get; set; } = [];
}

public sealed class OrderListItemDto
{
    public long Id { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public int ItemCount { get; set; }
}

public sealed class GetUserOrdersRequestDto
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetPaymentSummaryOrderDto
{
    public long Id { get; set; }
    public long ShoppingCartId { get; set; }
    public decimal TotalAmount { get; set; }

}
public record GetPreparePaymentRequestDto(
    string SuccessReturnUrl,string FailedReturnUrl
);

public sealed class GetPreparePaymentResponseDto
{
    public string GatewayUrl { get; init; } = default!;

    public long OrderTransactionId { get; init; }
}
public class CreateOrderRequestDto
{
    public long ShoppingCartId { get; set; }
    public string? ShippingAddress { get; set; }
    public string? MobileNumber { get; set; }
    //public string? TrackingNumber { get; set; }
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
