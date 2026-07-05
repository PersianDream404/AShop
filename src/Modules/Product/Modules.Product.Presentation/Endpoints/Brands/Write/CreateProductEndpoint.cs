using Framwork.Bus.Command;
using Framwork.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Product.Application.Contract.DTOs.Brands.Create;
using Modules.Product.Application.Contract.UseCase.Brands.Commands;
using SharedKernel.Constants;
using SharedKernel.Helper;
using SharedKernel.Interface;

namespace Modules.Product.Presentation.Endpoints.Brands.Write;


public static class CreateBrandEndpoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{ApiInfo.Prefix}", handler: async (


                    [FromBody]CreateBrandRequestDto request,
                  [FromServices] ICommandBus _commandBus, CancellationToken ct
                ) =>
            {

                var result = await _commandBus.Send<CreateBrandCommand, bool>
                                 (new CreateBrandCommand(request), ct);

                if (!result.IsSuccess)
                {
                    var message = result.GetErrorMessage();
                    return BadRequest(message);
                }

                return Ok(MessageHelper.Format(AppMessages.Create, AppEntity.Brand));


            })
                .WithTags(ApiInfo.Tag);
        }
    }
}
