using Framwork.Bus.Command;
using Framwork.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Product.Application.Contract.DTOs.ProductFeaturess.Create;
using Modules.Product.Application.Contract.UseCase.ProductFeaturess.Commands;
using SharedKernel.Constants;
using SharedKernel.Helper;
using SharedKernel.Interface;

namespace Modules.Product.Presentation.Endpoints.ProductFeaturess.Write;



public static class CreateProductFeaturesEndpoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{ApiInfo.Prefix}", handler: async (


                    [FromBody]CreateProductFeaturesRequestDto request,
                  [FromServices] ICommandBus _commandBus, CancellationToken ct ) =>
            {

                var result = await _commandBus.Send<CreateProductFeaturesCommand, bool>
                                 (new CreateProductFeaturesCommand(request), ct);

                if (!result.IsSuccess)
                {
                    var message = result.GetErrorMessage();
                    return BadRequest(message);
                }

                return Ok(MessageHelper.Format(AppMessages.Create, AppEntity.ProductFeatures));


            })
                .WithTags(ApiInfo.Tag);
        }
    }
}

