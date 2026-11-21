using Framwork.Bus.Command;
using Framwork.Extensions;
using Identity.Application.Contract.UseCase.Addresses.Commands;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using SharedKernel.Constants;
using SharedKernel.Helper;
using SharedKernel.Interface;

namespace Identity.Presentation.Endpoints.Addresses;

public static class DeleteAddressEndpoint
{
    public sealed class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete($"{ApiInfo.Prefix}/Addresses/{{addressId}}", async (
                long addressId,
                HttpContext httpContext,
                [FromServices] ICommandBus commandBus,
                CancellationToken cancellationToken) =>
            {
                if (!AddressEndpointHelpers.TryGetUserId(httpContext, out var userId))
                    return Unauthorized();

                var result = await commandBus.Send<DeleteAddressCommand, bool>(
                    new DeleteAddressCommand(userId, addressId), cancellationToken);

                if (!result.IsSuccess)
                    return BadRequest(result.GetErrorMessage());

                return Ok(MessageHelper.Format(AppMessages.Delete, AppEntity.Address));
            })
            .RequireAuthorization()
            .WithTags(ApiInfo.Tag);
        }
    }
}
