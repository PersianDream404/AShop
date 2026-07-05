using Framwork.Bus.Command;
using Framwork.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Product.Application.Contract.DTOs.FeaturesCategorys.Create;
using Modules.Product.Application.Contract.UseCase.FeaturesCategorys.Commands;
using SharedKernel.Constants;
using SharedKernel.Helper;
using SharedKernel.Interface;
using Modules.Product.Presentation.Endpoints.FeaturesCategory;

namespace Modules.Product.Presentation.Endpoints.FeaturesCategorys.Write;



public static class CreateFeaturesCategoryEndpoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{ApiInfo.Prefix}", handler: async (


                    [FromBody]CreateFeaturesCategoryRequestDto request,
                  [FromServices] ICommandBus _commandBus, CancellationToken ct ) =>
            {

                var result = await _commandBus.Send<CreateFeaturesCategoryCommand, bool>
                                 (new CreateFeaturesCategoryCommand(request), ct);

                if (!result.IsSuccess)
                {
                    var message = result.GetErrorMessage();
                    return BadRequest(message);
                }

                return Ok(MessageHelper.Format(AppMessages.Create, AppEntity.FeaturesCategory));


            })
                .WithTags(ApiInfo.Tag);
        }
    }
}

