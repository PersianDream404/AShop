using Framwork.Bus.Query;
using Framwork.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Order.Application.Contract.DTOs;
using Modules.Order.Application.Contract.UseCase.Orders.Queries;
using SharedKernel.Interface;
using Ardalis.Result;

namespace Modules.Order.Presentation.Endpoints.Orders.Read;

public static class GetOrderByIdEndpoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/{{orderId:long}}", handler: async (
                    HttpContext httpContext,
                    long orderId,
                    [FromServices] IQueryBus queryBus,
                    CancellationToken ct
                ) =>
            {
                if (!OrderEndpointHelpers.TryGetUserId(httpContext, out var userId))
                    return Unauthorized();

                var result = await queryBus.Send<GetOrderByIdQuery, OrderDto>(
                    new GetOrderByIdQuery(orderId, userId), ct);

                if (result.Status == ResultStatus.NotFound)
                    return NotFound(result.GetErrorMessage());

                if (result.Status == ResultStatus.Error)
                    return InternalServerError(result.GetErrorMessage());

                if (!result.IsSuccess)
                {
                    var message = result.GetErrorMessage();
                    return BadRequest(message);
                }

                return Ok(result.Value);
            })
                .RequireAuthorization()
                .WithTags(ApiInfo.Tag);
        }
    }
}
