using Framwork.Bus.Command;
using Framwork.Bus.Query;
using Framwork.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Payment.Application.Contract.DTOs;
using Modules.Payment.Application.Contract.Service;
using Modules.Payment.Application.Services;
using Parbad.Storage.Abstractions.Models;
using SharedKernel.Interface;
using System.Windows.Input;

namespace Modules.Payment.Presentation.Endpoints.Payments.Read;

public static class GetPaymentByIdEndpoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/{{paymentId:long}}/callback/", handler: async (
                    long paymentId,
                    [FromServices] IPaymentService paymentService,
                    [FromServices] IQueryBus queryBus,
                    [FromServices] ICommandBus commandBus,CancellationToken ct
                ) =>
            {
                var result = await paymentService.VerifyAsync(  new  VerifyPaymentRequestDto { PaymentId = paymentId },
                 ct);

                if (!result.IsSuccess)
                {
                    
                    return Results.Redirect("/payment-error");
                }


                return Results.Redirect(result.Value.ReturnUrl);
            
            })
                .WithTags(ApiInfo.Tag);
        }
    }
}
