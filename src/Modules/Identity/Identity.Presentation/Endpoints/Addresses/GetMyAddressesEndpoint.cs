using Framwork.Bus.Query;
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

public static class GetMyAddressesEndpoint
{
    public sealed class EndPoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/Addresses", async (
                HttpContext httpContext,
                [FromServices] IQueryBus queryBus,
                CancellationToken cancellationToken) =>
            {
                if (!AddressEndpointHelpers.TryGetUserId(httpContext, out var userId))
                    return Results.Unauthorized();

                var result = await queryBus.Send<GetMyAddressesQuery, IReadOnlyCollection<AddressDto>>(
                    new GetMyAddressesQuery(userId), cancellationToken);

                return result.IsSuccess
                    ? Results.Ok(result.Value)
                    : Results.BadRequest(result.GetErrorMessage());
            })
            .RequireAuthorization()
            .WithTags(ApiInfo.Tag);
        }
    }
}
