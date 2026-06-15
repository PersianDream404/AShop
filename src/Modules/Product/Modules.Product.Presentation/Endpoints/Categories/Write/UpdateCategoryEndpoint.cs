using Framwork.Bus.Command;
using Framwork.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Product.Application.Contract.DTOs.Categorys.Update;
using Modules.Product.Application.Contract.UseCase.Categorys.Commands;
using SharedKernel.Constants;
using SharedKernel.Helper;
using SharedKernel.Interface;

namespace Modules.Product.Presentation.Endpoints.Categorys.Write;



public static class UpdateCategoryEndpoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut($"{ApiInfo.Prefix}/{{id}}", handler: async (

                  long id,
                    [FromBody]UpdateCategoryRequestDto request,
                  [FromServices] ICommandBus _commandBus
                ) =>
            {

                if (id != request.Id)
                    return BadRequest(AppMessages.BadRequest);

                var result = await _commandBus.Send<UpdateCategoryCommand, bool>
                                 (new UpdateCategoryCommand(request));

                if (!result.IsSuccess)
                {
                    var message = result.GetErrorMessage();
                    return BadRequest(message);
                }

                return Ok(MessageHelper.Format(AppMessages.Edit, AppEntity.Category));


            })
                .WithTags(ApiInfo.Tag);
        }
    }
}