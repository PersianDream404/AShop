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

public static class CreateAddressEndpoint
{
    public sealed class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{ApiInfo.Prefix}/Addresses", async (
                [FromBody] AddressRequestDto request,
                HttpContext httpContext,
                [FromServices] ICommandBus commandBus,
                CancellationToken cancellationToken) =>
            {
                if (!AddressEndpointHelpers.TryGetUserId(httpContext, out var userId))
                    return Unauthorized();

                var result = await commandBus.Send<CreateAddressCommand, AddressDto>(
                    new CreateAddressCommand(userId, request), cancellationToken);

                if (!result.IsSuccess)
                    return BadRequest(result.GetErrorMessage());

                return Ok(result.Value, MessageHelper.Format(AppMessages.Create, AppEntity.Address));
            })
            .RequireAuthorization()
            .WithTags(ApiInfo.Tag);
        }
    }
}
