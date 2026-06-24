using Framwork.Bus.Query;
using Modules.Order.Application.Contract.DTOs;

namespace Modules.Order.Application.Contract.UseCase.Orders.Queries;

public record GetOrderByIdQuery(long OrderId) : IQuery<OrderDto>;
public record GetOrdersByUserIdQuery(long UserId) : IQuery<IEnumerable<OrderDto>>;
public record GetOrdersBySessionIdQuery(long SessionId) : IQuery<IEnumerable<OrderDto>>;
public record GetAllOrdersQuery() : IQuery<IEnumerable<OrderDto>>;
