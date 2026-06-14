using Framwork.Bus.Command;
using Framwork.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Product.Application.Contract.DTOs.FeaturesCategorys.Toggle;
using Modules.Product.Application.Contract.UseCase.FeaturesCategorys.Commands;
using SharedKernel.Constants;
using SharedKernel.Helper;
using SharedKernel.Interface;
using Modules.Product.Presentation.Endpoints.FeaturesCategory;
namespace Modules.Product.Presentation.Endpoints.FeaturesCategorys.Write;



public static class ToggleFeaturesCategoryEndpoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut($"{ApiInfo.Prefix}/{{id}}/Toggle", handler: async (

                  long id,
                 
                  [FromServices] ICommandBus _commandBus
                ) =>
            {


                var result = await _commandBus.Send<ToggleFeaturesCategoryCommand, bool>
                                 (new ToggleFeaturesCategoryCommand(id));

                if (!result.IsSuccess)
                {
                    var message = result.GetErrorMessage();
                    return BadRequest(message);
                }

                return Ok(MessageHelper.Format(AppMessages.Toggle, AppEntity.FeaturesCategory));


            })
                .WithTags(ApiInfo.Tag);
        }
    }
}