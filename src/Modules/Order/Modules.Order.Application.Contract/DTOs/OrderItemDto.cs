using Modules.Order.Domain.Enums;

namespace Modules.Order.Application.Contract.DTOs;

public class OrderItemDto
{
    public long Id { get; set; }
    public long OrderId { get; set; }
    public long ProductId { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal? DiscountValue { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateOrderItemRequestDto
{
    public long ProductId { get; set; }
    public int Quantity { get; set; }
    //public decimal? DiscountValue { get; set; }
}

public class UpdateOrderItemRequestDto
{
    public long Id { get; set; }
    public int Quantity { get; set; }
   // public decimal? DiscountValue { get; set; }
}
