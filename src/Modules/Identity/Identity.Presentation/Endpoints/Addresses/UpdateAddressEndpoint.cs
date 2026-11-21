using Framwork.Bus.Command;
using Framwork.Extensions;
using Identity.Application.Contract.DTOs.Addresses;
using Identity.Application.Contract.UseCase.Addresses;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ParsizCRM.API.Features.Account;
using SharedKernel.Interface;

namespace Identity.Presentation.Endpoints.Addresses;

public static class UpdateAddressEndpoint
{
    public sealed class EndPoint : IEndpoint
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
                    return Results.Unauthorized();

                var result = await commandBus.Send<UpdateAddressCommand, bool>(
                    new UpdateAddressCommand(userId, addressId, request), cancellationToken);

                return result.IsSuccess
                    ? Results.Ok()
                    : Results.BadRequest(result.GetErrorMessage());
            })
            .RequireAuthorization()
            .WithTags(ApiInfo.Tag);
        }
    }
}
