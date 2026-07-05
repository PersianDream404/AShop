using Framwork.Bus.Command;
using Framwork.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Product.Application.Contract.DTOs.FeaturesValuess.Create;
using Modules.Product.Application.Contract.UseCase.FeaturesValuess.Commands;
using SharedKernel.Constants;
using SharedKernel.Helper;
using SharedKernel.Interface;

namespace Modules.Product.Presentation.Endpoints.FeaturesValuess.Write;



public static class CreateFeaturesValuesEndpoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{ApiInfo.Prefix}", handler: async (


                    [FromBody]CreateFeaturesValuesRequestDto request,
                  [FromServices] ICommandBus _commandBus, CancellationToken ct ) =>
            {

                var result = await _commandBus.Send<CreateFeaturesValuesCommand, bool>
                                 (new CreateFeaturesValuesCommand(request), ct);

                if (!result.IsSuccess)
                {
                    var message = result.GetErrorMessage();
                    return BadRequest(message);
                }

                return Ok(MessageHelper.Format(AppMessages.Create, AppEntity.FeaturesValues));


            })
                .WithTags(ApiInfo.Tag);
        }
    }
}

