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

public static class PayOrderEndpoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{ApiInfo.Prefix}{{orderId:int}}/pay", handler: async (
                    [FromBody] GetPreparePaymentRequestDto request,long orderId,
                    [FromServices] ICommandBus commandBus, CancellationToken ct
                ) =>
            {
                var result = await commandBus.Send<PreparePaymentCommand, GetPreparePaymentResponseDto>(
                    new PreparePaymentCommand(orderId,request), ct);

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
