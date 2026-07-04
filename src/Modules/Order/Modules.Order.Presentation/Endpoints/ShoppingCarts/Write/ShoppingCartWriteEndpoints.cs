using Framwork.Bus.Command;
using Framwork.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Order.Application.Contract.DTOs;
using Modules.Order.Application.Contract.UseCase.ShoppingCarts.Commands;
using SharedKernel.Interface;

namespace Modules.Order.Presentation.Endpoints.ShoppingCarts.Write;

public static class CreateShoppingCartEndpoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{ApiInfo.Prefix}", handler: async (
                    [FromBody] CreateShoppingCartRequestDto request,
                    [FromServices] ICommandBus commandBus, CancellationToken ct
                ) =>
            {
                var result = await commandBus.Send<CreateShoppingCartCommand, long>(
                    new CreateShoppingCartCommand(request), ct);

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
