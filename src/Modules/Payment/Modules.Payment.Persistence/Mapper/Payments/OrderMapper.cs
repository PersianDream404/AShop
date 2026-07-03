using System.Linq;
using System.Linq.Expressions;

namespace Modules.Payment.Persistence.Mapper.Payments;

public static class PaymentMapper
{
    //public static Expression<Func<Domain.Entities.OrderEntity, OrderDto>> ToGetByIdDto()
    //{
    //    return x => new OrderDto
    //    {
    //        Id = x.Id,
    //        ShoppingCartId = x.ShoppingCartId,
    //        TotalAmount = x.TotalAmount,
    //        Status = x.Status,
    //        TaxAmount = x.TaxAmount,
    //        TermsAccepted = x.TermsAccepted,
    //        ShippingAddress = x.ShippingAddress,
    //        MobileNumber = x.MobileNumber,
    //        TrackingNumber = x.TrackingNumber,
    //        DisplayId = x.DisplayId,
    //        CreatedAt = x.CreatedAt,
    //        OrderItems = x.OrderItems.Select(oi => new OrderItemDto
    //        {
    //            Id = oi.Id,
    //            OrderId = oi.OrderId,
    //            ProductId = oi.ProductId,
    //            UnitPrice = oi.UnitPrice,
    //            Quantity = oi.Quantity,
    //            DiscountValue = oi.DiscountValue,
    //            TotalPrice = oi.TotalPrice,
    //            CreatedAt = oi.CreatedAt
    //        }).ToList()
    //    };
    //}
    //public static Expression<Func<Domain.Entities.OrderEntity, GetPaymentSummaryOrderDto>> ToGetPaymentSummaryByIdDto()
    //{
    //    return x => new GetPaymentSummaryOrderDto
    //    {
    //        Id = x.Id,
    //        ShoppingCartId = x.ShoppingCartId,
    //        TotalAmount = x.TotalAmount,
    //    };
    //}

    //public static Expression<Func<Domain.Entities.OrderEntity, OrderDto>> ToGetAllDto()
    //{
    //    return x => new OrderDto
    //    {
    //        Id = x.Id,
    //        ShoppingCartId = x.ShoppingCartId,
    //        TotalAmount = x.TotalAmount,
    //        Status = x.Status,
    //        TaxAmount = x.TaxAmount,
    //        TermsAccepted = x.TermsAccepted,
    //        ShippingAddress = x.ShippingAddress,
    //        MobileNumber = x.MobileNumber,
    //        TrackingNumber = x.TrackingNumber,
    //        DisplayId = x.DisplayId,
    //        CreatedAt = x.CreatedAt,
    //        OrderItems = x.OrderItems.Select(oi => new OrderItemDto
    //        {
    //            Id = oi.Id,
    //            OrderId = oi.OrderId,
    //            ProductId = oi.ProductId,
    //            UnitPrice = oi.UnitPrice,
    //            Quantity = oi.Quantity,
    //            DiscountValue = oi.DiscountValue,
    //            TotalPrice = oi.TotalPrice,
    //            CreatedAt = oi.CreatedAt
    //        }).ToList()
    //    };
    //}
}
