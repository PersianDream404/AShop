using Ardalis.Result;
using Framwork.Bus.Command;
using Framwork.Extensions;
using Identity.Application.Contract.DTOs.Users;
using Identity.Application.Contract.UseCase.Users.Commands;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ParsizCRM.API.Features.Account;
using SharedKernel.Interface;
using AddressEndpointHelpers = Identity.Presentation.Endpoints.Addresses.AddressEndpointHelpers;

namespace Identity.Presentation.Users.Create;

public static class UpdateMyProfileEndpoint
{
    public sealed class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut($"{ApiInfo.Prefix}/Profile", async (
                [FromBody] UpdateMyProfileRequestDto request,
                HttpContext httpContext,
                [FromServices] ICommandBus commandBus,
                CancellationToken cancellationToken) =>
            {
                if (!AddressEndpointHelpers.TryGetUserId(httpContext, out var userId))
                    return Unauthorized();

                var result = await commandBus.Send<UpdateMyProfileCommand, UserProfileDto>(
                    new UpdateMyProfileCommand(userId, request), cancellationToken);

                if (result.Status == ResultStatus.NotFound)
                    return NotFound(result.GetErrorMessage());

                if (!result.IsSuccess)
                    return BadRequest(result.GetErrorMessage());

                return Ok(result.Value, "پروفایل با موفقیت ویرایش شد");
            })
            .RequireAuthorization()
            .WithTags(ApiInfo.Tag);
        }
    }
}
