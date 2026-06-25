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

public static class UpdateOrderItemEndpoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut($"{ApiInfo.Prefix}/{{orderId}}/items", handler: async (
                    long orderId,
                    [FromBody] UpdateOrderItemRequestDto request,
                    [FromServices] ICommandBus commandBus
                ) =>
            {
                var result = await commandBus.Send<UpdateOrderItemCommand, bool>(
                    new UpdateOrderItemCommand(orderId, request));

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
