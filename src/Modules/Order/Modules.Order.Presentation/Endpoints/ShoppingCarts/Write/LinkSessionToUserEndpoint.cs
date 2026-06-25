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

public static class LinkSessionToUserEndpoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut($"{ApiInfo.Prefix}/{{cartId}}/link-user", handler: async (
                    long cartId,
                    [FromBody] LinkSessionToUserRequestDto request,
                    [FromServices] ICommandBus commandBus
                ) =>
            {
                request.CartId = cartId;
                var result = await commandBus.Send<LinkSessionToUserCommand, bool>(
                    new LinkSessionToUserCommand(request));

                if (!result.IsSuccess)
                {
                    var message = result.GetErrorMessage();
                    return BadRequest(message);
                }

                return Ok("سبد خرید به کاربر متصل شد");
            })
       
                .WithTags(ApiInfo.Tag);
        }
    }
}
