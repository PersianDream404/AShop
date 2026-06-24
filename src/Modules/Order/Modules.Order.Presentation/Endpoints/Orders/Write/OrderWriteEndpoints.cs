using Framwork.Bus.Command;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Order.Application.Contract.DTOs;
using Modules.Order.Application.Contract.UseCase.Orders.Commands;
using SharedKernel.Interface;

namespace Modules.Order.Presentation.Endpoints.Orders.Write;

public class CreateOrderApiInfo
{
    public const string Prefix = "/api/orders";
    public const string Tag = "Orders";
}

public static class CreateOrderEndpoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{CreateOrderApiInfo.Prefix}", handler: async (
                    [FromBody] CreateOrderRequestDto request,
                    [FromServices] ICommandBus commandBus
                ) =>
            {
                var result = await commandBus.Send<CreateOrderCommand, bool>(
                    new CreateOrderCommand(request));

                if (!result.IsSuccess)
                {
                    var message = result.Errors?.FirstOrDefault()?.Description ?? "Error creating order";
                    return BadRequest(message);
                }

                return Ok(new { message = "Order created successfully" });
            })
                .WithName("CreateOrder")
                .WithOpenApi()
                .WithTags(CreateOrderApiInfo.Tag);
        }
    }
}

public static class UpdateOrderStatusEndpoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut($"{CreateOrderApiInfo.Prefix}/{{orderId}}/status", handler: async (
                    long orderId,
                    [FromBody] UpdateOrderStatusRequestDto request,
                    [FromServices] ICommandBus commandBus
                ) =>
            {
                request.OrderId = orderId;
                var result = await commandBus.Send<UpdateOrderStatusCommand, bool>(
                    new UpdateOrderStatusCommand(request));

                if (!result.IsSuccess)
                {
                    var message = result.Errors?.FirstOrDefault()?.Description ?? "Error updating order status";
                    return BadRequest(message);
                }

                return Ok(new { message = "Order status updated successfully" });
            })
                .WithName("UpdateOrderStatus")
                .WithOpenApi()
                .WithTags(CreateOrderApiInfo.Tag);
        }
    }
}

public static class UpdateTrackingNumberEndpoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut($"{CreateOrderApiInfo.Prefix}/{{orderId}}/tracking", handler: async (
                    long orderId,
                    [FromBody] UpdateTrackingNumberRequestDto request,
                    [FromServices] ICommandBus commandBus
                ) =>
            {
                request.OrderId = orderId;
                var result = await commandBus.Send<UpdateTrackingNumberCommand, bool>(
                    new UpdateTrackingNumberCommand(request));

                if (!result.IsSuccess)
                {
                    var message = result.Errors?.FirstOrDefault()?.Description ?? "Error updating tracking number";
                    return BadRequest(message);
                }

                return Ok(new { message = "Tracking number updated successfully" });
            })
                .WithName("UpdateTrackingNumber")
                .WithOpenApi()
                .WithTags(CreateOrderApiInfo.Tag);
        }
    }
}

public static class AddOrderItemEndpoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{CreateOrderApiInfo.Prefix}/{{orderId}}/items", handler: async (
                    long orderId,
                    [FromBody] CreateOrderItemRequestDto request,
                    [FromServices] ICommandBus commandBus
                ) =>
            {
                var result = await commandBus.Send<AddOrderItemCommand, bool>(
                    new AddOrderItemCommand(orderId, request));

                if (!result.IsSuccess)
                {
                    var message = result.Errors?.FirstOrDefault()?.Description ?? "Error adding order item";
                    return BadRequest(message);
                }

                return Ok(new { message = "Item added to order successfully" });
            })
                .WithName("AddOrderItem")
                .WithOpenApi()
                .WithTags(CreateOrderApiInfo.Tag);
        }
    }
}

public static class RemoveOrderItemEndpoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete($"{CreateOrderApiInfo.Prefix}/items/{{itemId}}", handler: async (
                    long itemId,
                    [FromServices] ICommandBus commandBus
                ) =>
            {
                var result = await commandBus.Send<RemoveOrderItemCommand, bool>(
                    new RemoveOrderItemCommand(itemId));

                if (!result.IsSuccess)
                {
                    var message = result.Errors?.FirstOrDefault()?.Description ?? "Error removing order item";
                    return BadRequest(message);
                }

                return Ok(new { message = "Item removed from order successfully" });
            })
                .WithName("RemoveOrderItem")
                .WithOpenApi()
                .WithTags(CreateOrderApiInfo.Tag);
        }
    }
}

public static class UpdateOrderItemEndpoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut($"{CreateOrderApiInfo.Prefix}/{{orderId}}/items", handler: async (
                    long orderId,
                    [FromBody] UpdateOrderItemRequestDto request,
                    [FromServices] ICommandBus commandBus
                ) =>
            {
                var result = await commandBus.Send<UpdateOrderItemCommand, bool>(
                    new UpdateOrderItemCommand(orderId, request));

                if (!result.IsSuccess)
                {
                    var message = result.Errors?.FirstOrDefault()?.Description ?? "Error updating order item";
                    return BadRequest(message);
                }

                return Ok(new { message = "Order item updated successfully" });
            })
                .WithName("UpdateOrderItem")
                .WithOpenApi()
                .WithTags(CreateOrderApiInfo.Tag);
        }
    }
}
