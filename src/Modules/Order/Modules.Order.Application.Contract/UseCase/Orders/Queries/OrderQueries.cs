using Framwork.Bus.Query;
using Framwork.PagedList;
using Modules.Order.Application.Contract.DTOs;

namespace Modules.Order.Application.Contract.UseCase.Orders.Queries;

public record GetOrderByIdQuery(long OrderId, long UserId) : IQuery<OrderDto>;
public record GetOrdersByUserIdQuery(long UserId, int PageNumber, int PageSize) : IQuery<PagedList<OrderListItemDto>>;
public record GetOrdersBySessionIdQuery(Guid SessionId) : IQuery<OrderDto>;
public record GetAllOrdersQuery() : IQuery<IEnumerable<OrderDto>>;
public record GetPaymentSummaryByIdQuery(long OrderId) : IQuery<GetPaymentSummaryOrderDto>;
