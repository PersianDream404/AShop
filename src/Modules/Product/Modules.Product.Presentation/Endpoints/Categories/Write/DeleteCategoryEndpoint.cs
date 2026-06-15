using Framwork.Bus.Command;
using Framwork.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Product.Application.Contract.UseCase.Categorys.Commands;
using SharedKernel.Constants;
using SharedKernel.Helper;
using SharedKernel.Interface;

namespace Modules.Product.Presentation.Endpoints.Categorys.Write;



public static class DeleteCategoryEndpoint
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


                var result = await _commandBus.Send<DeleteCategoryCommand, bool>
                                 (new DeleteCategoryCommand(id));

                if (!result.IsSuccess)
                {
                    var message = result.GetErrorMessage();
                    return BadRequest(message);
                }

                return Ok(MessageHelper.Format(AppMessages.Delete, AppEntity.Category));


            })
                .WithTags(ApiInfo.Tag);
        }
    }
}