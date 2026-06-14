using Framwork.Bus.Command;
using Framwork.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Product.Application.Contract.DTOs.ProductFeaturess.Update;
using Modules.Product.Application.Contract.UseCase.ProductFeaturess.Commands;
using SharedKernel.Constants;
using SharedKernel.Helper;
using SharedKernel.Interface;

namespace Modules.Product.Presentation.Endpoints.ProductFeaturess.Write;



public static class UpdateProductFeaturesEndpoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut($"{ApiInfo.Prefix}/{{id}}", handler: async (

                  long id,
                    [FromBody]UpdateProductFeaturesRequestDto request,
                  [FromServices] ICommandBus _commandBus
                ) =>
            {

                if (id != request.Id)
                    return BadRequest(AppMessages.BadRequest);

                var result = await _commandBus.Send<UpdateProductFeaturesCommand, bool>
                                 (new UpdateProductFeaturesCommand(request));

                if (!result.IsSuccess)
                {
                    var message = result.GetErrorMessage();
                    return BadRequest(message);
                }

                return Ok(MessageHelper.Format(AppMessages.Edit, AppEntity.ProductFeatures));


            })
                .WithTags(ApiInfo.Tag);
        }
    }
}