using Framwork.Bus.Command;
using Framwork.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Order.Application.Contract.Resources.Orders;
using Modules.Order.Application.Contract.UseCase.Orders.Commands;
using SharedKernel.Interface;

namespace Modules.Order.Presentation.Endpoints.Orders.Write;

public static class RemoveOrderItemEndpoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete($"{ApiInfo.Prefix}/items/{{itemId}}", handler: async (
                    long itemId,
                    [FromServices] ICommandBus commandBus
                ) =>
            {
                var result = await commandBus.Send<RemoveOrderItemCommand, bool>(
                    new RemoveOrderItemCommand(itemId));

                if (!result.IsSuccess)
                {
                    var message = result.GetErrorMessage();
                    return BadRequest(message);
                }


                return Ok(OrderValidationMessages.RemoveOrderItem);
            })
                .WithTags(ApiInfo.Tag);
        }
    }
}
