using Framwork.Bus.Command;
using Framwork.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Product.Application.Contract.DTOs.FeaturesCategorys.Update;
using Modules.Product.Application.Contract.UseCase.FeaturesCategorys.Commands;
using Modules.Product.Presentation.Endpoints.FeaturesCategory;
using SharedKernel.Constants;
using SharedKernel.Helper;
using SharedKernel.Interface;

namespace Modules.Product.Presentation.Endpoints.FeaturesCategorys.Write;



public static class UpdateFeaturesCategoryEndpoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut($"{ApiInfo.Prefix}/{{id}}", handler: async (

                  long id,
                    [FromBody]UpdateFeaturesCategoryRequestDto request,
                  [FromServices] ICommandBus _commandBus, CancellationToken ct ) =>
            {

                if (id != request.Id)
                    return BadRequest(AppMessages.BadRequest);

                var result = await _commandBus.Send<UpdateFeaturesCategoryCommand, bool>
                                 (new UpdateFeaturesCategoryCommand(request), ct);

                if (!result.IsSuccess)
                {
                    var message = result.GetErrorMessage();
                    return BadRequest(message);
                }

                return Ok(MessageHelper.Format(AppMessages.Edit, AppEntity.FeaturesCategory));


            })
                .WithTags(ApiInfo.Tag);
        }
    }
}

