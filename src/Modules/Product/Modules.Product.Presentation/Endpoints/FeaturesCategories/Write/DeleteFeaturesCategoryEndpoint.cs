using Framwork.Bus.Command;
using Framwork.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Product.Application.Contract.UseCase.FeaturesCategorys.Commands;
using SharedKernel.Constants;
using SharedKernel.Helper;
using SharedKernel.Interface;
using Modules.Product.Presentation.Endpoints.FeaturesCategory;
namespace Modules.Product.Presentation.Endpoints.FeaturesCategorys.Write;



public static class DeleteFeaturesCategoryEndpoint
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


                var result = await _commandBus.Send<DeleteFeaturesCategoryCommand, bool>
                                 (new DeleteFeaturesCategoryCommand(id));

                if (!result.IsSuccess)
                {
                    var message = result.GetErrorMessage();
                    return BadRequest(message);
                }

                return Ok(MessageHelper.Format(AppMessages.Delete, AppEntity.FeaturesCategory));


            })
                .WithTags(ApiInfo.Tag);
        }
    }
}