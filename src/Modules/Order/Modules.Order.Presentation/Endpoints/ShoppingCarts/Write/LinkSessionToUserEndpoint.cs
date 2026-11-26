using Framwork.Bus.Command;
using Framwork.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Order.Application.Contract.UseCase.ShoppingCarts.Commands;
using SharedKernel.Interface;
using System.Security.Claims;

namespace Modules.Order.Presentation.Endpoints.ShoppingCarts.Write;

public static class LinkSessionToUserEndpoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut($"{ApiInfo.Prefix}/{{cartId}}/link-user", handler: async (
                    long cartId,
                    HttpContext httpContext,
                    [FromServices] ICommandBus commandBus, CancellationToken ct
                ) =>
            {
                var userIdValue = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)
                                  ?? httpContext.User.FindFirstValue("UserId");

                if (!long.TryParse(userIdValue, out var userId))
                    return Unauthorized();

                var result = await commandBus.Send<LinkSessionToUserCommand, bool>(
                    new LinkSessionToUserCommand(cartId, userId), ct);

                if (!result.IsSuccess)
                {
                    var message = result.GetErrorMessage();
                    return BadRequest(message);
                }

                return Ok("سبد خرید به کاربر متصل شد");
            })
                .RequireAuthorization()
                .WithTags(ApiInfo.Tag);
        }
    }
}
