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

public static class CreateAddressEndpoint
{
    public sealed class EndPoint : IEndpoint
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
                    return Results.Unauthorized();

                var result = await commandBus.Send<CreateAddressCommand, AddressDto>(
                    new CreateAddressCommand(userId, request), cancellationToken);

                return result.IsSuccess
                    ? Results.Ok(result.Value)
                    : Results.BadRequest(result.GetErrorMessage());
            })
            .RequireAuthorization()
            .WithTags(ApiInfo.Tag);
        }
    }
}
