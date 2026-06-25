using Framwork.Bus.Command;
using Framwork.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Order.Application.Contract.DTOs;
using Modules.Order.Application.Contract.UseCase.Orders.Commands;
using SharedKernel.Interface;

namespace Modules.Order.Presentation.Endpoints.Orders.Write;

public static class CreateOrderEndpoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{ApiInfo.Prefix}", handler: async (
                    [FromBody] CreateOrderRequestDto request,
                    [FromServices] ICommandBus commandBus
                ) =>
            {
                var result = await commandBus.Send<CreateOrderCommand, long>(
                    new CreateOrderCommand(request));

                if (!result.IsSuccess)
                {
                    var message = result.GetErrorMessage();
                    return BadRequest(message);
                }

                return Ok(result.Value);
            })
                .WithTags(ApiInfo.Tag);
        }
    }
}
