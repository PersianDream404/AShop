using Framwork.Bus.Query;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modules.Order.Application.Contract.DTOs;
using Modules.Order.Application.Contract.UseCase.Orders.Queries;
using SharedKernel.Interface;

namespace Modules.Order.Presentation.Endpoints.Orders.Read;

public static class GetOrderByIdEndpoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/orders/{orderId}", handler: async (
                    long orderId,
                    [FromServices] IQueryBus queryBus
                ) =>
            {
                var result = await queryBus.Send<GetOrderByIdQuery, OrderDto>(
                    new GetOrderByIdQuery(orderId));

                if (!result.IsSuccess)
                {
                    var message = result.Errors?.FirstOrDefault()?.Description ?? "Order not found";
                    return BadRequest(message);
                }

                return Ok(result.Value);
            })
                .WithName("GetOrderById")
                .WithOpenApi()
                .WithTags("Orders");
        }
    }
}

public static class GetOrdersByUserIdEndpoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/orders/user/{userId}", handler: async (
                    long userId,
                    [FromServices] IQueryBus queryBus
                ) =>
            {
                var result = await queryBus.Send<GetOrdersByUserIdQuery, IEnumerable<OrderDto>>(
                    new GetOrdersByUserIdQuery(userId));

                if (!result.IsSuccess)
                {
                    var message = result.Errors?.FirstOrDefault()?.Description ?? "Error retrieving orders";
                    return BadRequest(message);
                }

                return Ok(result.Value);
            })
                .WithName("GetOrdersByUserId")
                .WithOpenApi()
                .WithTags("Orders");
        }
    }
}

public static class GetOrdersBySessionIdEndpoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/orders/session/{sessionId}", handler: async (
                    long sessionId,
                    [FromServices] IQueryBus queryBus
                ) =>
            {
                var result = await queryBus.Send<GetOrdersBySessionIdQuery, IEnumerable<OrderDto>>(
                    new GetOrdersBySessionIdQuery(sessionId));

                if (!result.IsSuccess)
                {
                    var message = result.Errors?.FirstOrDefault()?.Description ?? "Error retrieving orders";
                    return BadRequest(message);
                }

                return Ok(result.Value);
            })
                .WithName("GetOrdersBySessionId")
                .WithOpenApi()
                .WithTags("Orders");
        }
    }
}

public static class GetAllOrdersEndpoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/orders", handler: async (
                    [FromServices] IQueryBus queryBus
                ) =>
            {
                var result = await queryBus.Send<GetAllOrdersQuery, IEnumerable<OrderDto>>(
                    new GetAllOrdersQuery());

                if (!result.IsSuccess)
                {
                    var message = result.Errors?.FirstOrDefault()?.Description ?? "Error retrieving orders";
                    return BadRequest(message);
                }

                return Ok(result.Value);
            })
                .WithName("GetAllOrders")
                .WithOpenApi()
                .WithTags("Orders");
        }
    }
}
