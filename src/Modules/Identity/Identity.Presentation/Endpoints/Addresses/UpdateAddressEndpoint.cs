using Framwork.Bus.Command;
using Framwork.Extensions;
using Identity.Application.Contract.DTOs.Addresses;
using Identity.Application.Contract.UseCase.Addresses.Commands;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using SharedKernel.Constants;
using SharedKernel.Helper;
using SharedKernel.Interface;

namespace Identity.Presentation.Endpoints.Addresses;

public static class UpdateAddressEndpoint
{
    public sealed class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut($"{ApiInfo.Prefix}/Addresses/{{addressId}}", async (
                long addressId,
                [FromBody] AddressRequestDto request,
                HttpContext httpContext,
                [FromServices] ICommandBus commandBus,
                CancellationToken cancellationToken) =>
            {
                if (!AddressEndpointHelpers.TryGetUserId(httpContext, out var userId))
                    return Unauthorized();

                var result = await commandBus.Send<UpdateAddressCommand, bool>(
                    new UpdateAddressCommand(userId, addressId, request), cancellationToken);

                if (!result.IsSuccess)
                    return BadRequest(result.GetErrorMessage());

                return Ok(MessageHelper.Format(AppMessages.Edit, AppEntity.Address));
            })
            .RequireAuthorization()
            .WithTags(ApiInfo.Tag);
        }
    }
}
