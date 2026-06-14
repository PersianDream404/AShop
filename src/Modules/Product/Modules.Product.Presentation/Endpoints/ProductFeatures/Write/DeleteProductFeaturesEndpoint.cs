using Framwork.Bus.Command;
using Framwork.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Product.Application.Contract.UseCase.ProductFeaturess.Commands;
using SharedKernel.Constants;
using SharedKernel.Helper;
using SharedKernel.Interface;

namespace Modules.Product.Presentation.Endpoints.ProductFeaturess.Write;



public static class DeleteProductFeaturesEndpoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete($"{ApiInfo.Prefix}/{{id}}", handler: async (

                  long id,
                   
                  [FromServices] ICommandBus _commandBus
                ) =>
            {


                var result = await _commandBus.Send<DeleteProductFeaturesCommand, bool>
                                 (new DeleteProductFeaturesCommand(id));

                if (!result.IsSuccess)
                {
                    var message = result.GetErrorMessage();
                    return BadRequest(message);
                }

                return Ok(MessageHelper.Format(AppMessages.Delete, AppEntity.ProductFeatures));


            })
                .WithTags(ApiInfo.Tag);
        }
    }
}