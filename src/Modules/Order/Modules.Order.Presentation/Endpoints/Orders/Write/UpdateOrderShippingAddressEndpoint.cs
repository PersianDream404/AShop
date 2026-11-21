using Framwork.Bus.Command;
using Framwork.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Order.Application.Contract.DTOs.Orders;
using Modules.Order.Application.Contract.UseCase.Orders.Commands;
using SharedKernel.Constants;
using SharedKernel.Helper;
using SharedKernel.Interface;
using System.Security.Claims;

namespace Modules.Order.Presentation.Endpoints.Orders.Write;

public static class UpdateOrderShippingAddressEndpoint
{
    public sealed class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut($"{ApiInfo.Prefix}/{{orderId}}/shipping-address", async (
                long orderId,
                [FromBody] UpdateOrderShippingAddressRequestDto request,
                HttpContext httpContext,
                [FromServices] ICommandBus commandBus,
                CancellationToken cancellationToken) =>
            {
                var userIdValue = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)
                                  ?? httpContext.User.FindFirstValue("UserId");

                if (!long.TryParse(userIdValue, out var userId))
                    return Unauthorized();

                var result = await commandBus.Send<UpdateOrderShippingAddressCommand, bool>(
                    new UpdateOrderShippingAddressCommand(userId, orderId, request.AddressId),
                    cancellationToken);

                if (!result.IsSuccess)
                    return BadRequest(result.GetErrorMessage());

                return Ok(MessageHelper.Format(AppMessages.Edit, AppEntity.Order));
            })
            .RequireAuthorization()
            .WithTags(ApiInfo.Tag);
        }
    }
}
