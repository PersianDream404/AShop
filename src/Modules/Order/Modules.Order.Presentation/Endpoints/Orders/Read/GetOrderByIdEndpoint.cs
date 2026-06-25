using Framwork.Bus.Command;
using Framwork.Bus.Query;
using Framwork.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Order.Application.Contract.DTOs;
using Modules.Order.Application.Contract.UseCase.Orders.Commands;
using Modules.Order.Application.Contract.UseCase.Orders.Queries;
using SharedKernel.Interface;
using System.Windows.Input;

namespace Modules.Order.Presentation.Endpoints.Orders.Read;

public static class GetOrderByIdEndpoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/{{orderId}}", handler: async (
                    long orderId,
                    [FromServices] IQueryBus queryBus,
                    [FromServices] ICommandBus commandBus
                ) =>
            {
                var resultUpdateOrderTotalAmount = await commandBus.Send<UpdateOrderTotalAmountCommand, bool>(
                   new UpdateOrderTotalAmountCommand(orderId));

                var result = await queryBus.Send<GetOrderByIdQuery, OrderDto>(
                    new GetOrderByIdQuery(orderId));

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
