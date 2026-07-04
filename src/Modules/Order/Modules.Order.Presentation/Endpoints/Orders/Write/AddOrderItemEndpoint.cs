using Framwork.Bus.Command;
using Framwork.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Order.Application.Contract.DTOs;
using Modules.Order.Application.Contract.Resources.Orders;
using Modules.Order.Application.Contract.UseCase.Orders.Commands;
using SharedKernel.Interface;

namespace Modules.Order.Presentation.Endpoints.Orders.Write;

public static class AddOrderItemEndpoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{ApiInfo.Prefix}/{{orderId}}/items", handler: async (
                    long orderId,
                    [FromBody] CreateOrderItemRequestDto request,
                    [FromServices] ICommandBus commandBus, CancellationToken ct
                ) =>
            {
                var result = await commandBus.Send<AddOrderItemCommand, bool>(
                    new AddOrderItemCommand(orderId, request), ct);

                if (!result.IsSuccess)
                {
                    var message = result.GetErrorMessage();
                    return BadRequest(message);
                }

                return Ok(OrderValidationMessages.AddOrderItem);
            })
                .WithTags(ApiInfo.Tag);
        }
    }
}
