using Framwork.Bus.Command;
using Framwork.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Order.Application.Contract.DTOs;
using Modules.Order.Application.Contract.UseCase.Orders.Commands;
using SharedKernel.Interface;

namespace Modules.Order.Presentation.Endpoints.Orders.Write;

//public static class UpdateOrderStatusEndpoint
//{
//    public class EndPoint : BaseEndpoint, IEndpoint
//    {
//        public void MapEndpoint(IEndpointRouteBuilder app)
//        {
//            app.MapPut($"{ApiInfo.Prefix}/{{orderId}}/status", handler: async (
//                    long orderId,
//                    [FromBody] UpdateOrderStatusRequestDto request,
//                    [FromServices] ICommandBus commandBus
//                ) =>
//            {
//                request.OrderId = orderId;

//                var result = await commandBus.Send<UpdateOrderStatusCommand, bool>(
//                    new UpdateOrderStatusCommand(request));

//                if (!result.IsSuccess)
//                {
//                    var message = result.GetErrorMessage();
//                    return BadRequest(message);
//                }

//                return Ok(new { message = "Order status updated successfully" });
//            })
//                .WithTags(ApiInfo.Tag);
//        }
//    }
//}
