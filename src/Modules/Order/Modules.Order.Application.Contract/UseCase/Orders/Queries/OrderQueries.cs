using Framwork.Bus.Query;
using Modules.Order.Application.Contract.DTOs;

namespace Modules.Order.Application.Contract.UseCase.Orders.Queries;

public record GetOrderByIdQuery(long OrderId) : IQuery<OrderDto>;
public record GetOrdersByUserIdQuery(long UserId) : IQuery<OrderDto>;
public record GetOrdersBySessionIdQuery(Guid SessionId) : IQuery<OrderDto>;
public record GetAllOrdersQuery() : IQuery<IEnumerable<OrderDto>>;
