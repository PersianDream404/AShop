using Ardalis.Result;
using Framwork.Bus.Query;
using Framwork.Extensions;
using Framwork.PagedList;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Order.Application.Contract.DTOs;
using Modules.Order.Application.Contract.UseCase.Orders.Queries;
using SharedKernel.Interface;

namespace Modules.Order.Presentation.Endpoints.Orders.Read;

public static class GetOrdersByUserIdEndpoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/user", handler: async (
                    HttpContext httpContext,
                    [AsParameters] GetUserOrdersRequestDto request,
                    [FromServices] IQueryBus queryBus,
                    CancellationToken ct
                ) =>
            {
                if (!OrderEndpointHelpers.TryGetUserId(httpContext, out var userId))
                    return Unauthorized();

                var result = await queryBus.Send<GetOrdersByUserIdQuery, PagedList<OrderListItemDto>>(
                    new GetOrdersByUserIdQuery(userId, request.PageNumber, request.PageSize), ct);

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
