using Ardalis.Result;
using Framwork.Bus.Query;
using Framwork.Extensions;
using Identity.Application.Contract.DTOs.Users;
using Identity.Application.Contract.UseCase.Users.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ParsizCRM.API.Features.Account;
using SharedKernel.Interface;
using AddressEndpointHelpers = Identity.Presentation.Endpoints.Addresses.AddressEndpointHelpers;

namespace Identity.Presentation.Users.Create;

public static class GetMyProfileEndpoint
{
    public sealed class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/Profile", async (
                HttpContext httpContext,
                [FromServices] IQueryBus queryBus,
                CancellationToken cancellationToken) =>
            {
                if (!AddressEndpointHelpers.TryGetUserId(httpContext, out var userId))
                    return Unauthorized();

                var result = await queryBus.Send<GetMyProfileQuery, UserProfileDto>(
                    new GetMyProfileQuery(userId), cancellationToken);

                if (result.Status == ResultStatus.NotFound)
                    return NotFound(result.GetErrorMessage());

                if (!result.IsSuccess)
                    return BadRequest(result.GetErrorMessage());

                return Ok(result.Value);
            })
            .RequireAuthorization()
            .WithTags(ApiInfo.Tag);
        }
    }
}
