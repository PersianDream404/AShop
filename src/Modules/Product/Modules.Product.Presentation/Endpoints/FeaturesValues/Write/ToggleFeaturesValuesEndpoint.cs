using Framwork.Bus.Command;
using Framwork.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Product.Application.Contract.DTOs.FeaturesValuess.Toggle;
using Modules.Product.Application.Contract.UseCase.FeaturesValuess.Commands;
using SharedKernel.Constants;
using SharedKernel.Helper;
using SharedKernel.Interface;

namespace Modules.Product.Presentation.Endpoints.FeaturesValuess.Write;



public static class ToggleFeaturesValuesEndpoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut($"{ApiInfo.Prefix}/{{id}}/Toggle", handler: async (

                  long id,
                 
                  [FromServices] ICommandBus _commandBus, CancellationToken ct ) =>
            {


                var result = await _commandBus.Send<ToggleFeaturesValuesCommand, bool>
                                 (new ToggleFeaturesValuesCommand(id), ct);

                if (!result.IsSuccess)
                {
                    var message = result.GetErrorMessage();
                    return BadRequest(message);
                }

                return Ok(MessageHelper.Format(AppMessages.Toggle, AppEntity.FeaturesValues));


            })
                .WithTags(ApiInfo.Tag);
        }
    }
}

