using Framwork.Bus.Command;
using Framwork.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Product.Application.Contract.DTOs.Products.Create;
using Modules.Product.Application.Contract.UseCase.Products.Commands;
using ParsizCRM.API.Features.Account;
using SharedKernel.Constants;
using SharedKernel.Interface;

namespace Modules.Product.Presentation.Endpoints.Products.Write;



public static class CreateProductEndpoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{ApiInfo.Prefix}", handler: async (


                    [FromBody]CreateProductRequestDto request,
                  [FromServices] ICommandBus _commandBus
                ) =>
            {

                var result = await _commandBus.Send<CreateProductCommand, bool>
                                 (new CreateProductCommand(request));

                if (!result.IsSuccess)
                {
                    var message = result.GetErrorMessage();
                    return BadRequest(message);
                }

                return Ok(AppMessages.Create);


            })
                .WithTags(ApiInfo.Tag);
        }
    }
}