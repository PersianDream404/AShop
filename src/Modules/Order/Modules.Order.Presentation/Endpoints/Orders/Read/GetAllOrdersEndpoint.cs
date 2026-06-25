using Framwork.Bus.Query;
using Framwork.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Order.Application.Contract.DTOs;
using Modules.Order.Application.Contract.UseCase.Orders.Queries;
using SharedKernel.Interface;

namespace Modules.Order.Presentation.Endpoints.Orders.Read;

public static class GetAllOrdersEndpoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}", handler: async (
                    [FromServices] IQueryBus queryBus
                ) =>
            {
                var result = await queryBus.Send<GetAllOrdersQuery, IEnumerable<OrderDto>>(
                    new GetAllOrdersQuery());

                if (!result.IsSuccess)
                {
                    var message = result.GetErrorMessage();
                    return BadRequest(message);
                }

                return Ok(result.Value);
            })
                .WithTags(ApiInfo.Tag);
        }
    }
}
