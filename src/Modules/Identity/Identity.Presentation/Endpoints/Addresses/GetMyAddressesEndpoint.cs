using Framwork.Bus.Query;
using Framwork.Extensions;
using Identity.Application.Contract.DTOs.Addresses;
using Identity.Application.Contract.UseCase.Addresses.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using SharedKernel.Interface;

namespace Identity.Presentation.Endpoints.Addresses;

public static class GetMyAddressesEndpoint
{
    public sealed class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/Addresses", async (
                HttpContext httpContext,
                [FromServices] IQueryBus queryBus,
                CancellationToken cancellationToken) =>
            {
                if (!AddressEndpointHelpers.TryGetUserId(httpContext, out var userId))
                    return Unauthorized();

                var result = await queryBus.Send<GetMyAddressesQuery, IReadOnlyCollection<AddressDto>>(
                    new GetMyAddressesQuery(userId), cancellationToken);

                if (!result.IsSuccess)
                    return BadRequest(result.GetErrorMessage());

                return Ok(result.Value);
            })
            .RequireAuthorization()
            .WithTags(ApiInfo.Tag);
        }
    }
}
