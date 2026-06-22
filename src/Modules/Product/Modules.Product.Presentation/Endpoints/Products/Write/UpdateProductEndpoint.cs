using Framwork.Bus.Command;
using Framwork.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Product.Application.Contract.DTOs.Products.Create;
using Modules.Product.Application.Contract.UseCase.Products.Commands;
using SharedKernel.Constants;
using SharedKernel.Helper;
using SharedKernel.Interface;

namespace Modules.Product.Presentation.Endpoints.Products.Write;



public static class UpdateProductEndpoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut($"{ApiInfo.Prefix}/{{id}}", handler: async (

                  long id,
                    [FromBody]UpdateProductRequestDto request,
                  [FromServices] ICommandBus _commandBus
                ) =>
            {

                if (id != request.Id)
                    return BadRequest(AppMessages.BadRequest);

                var result = await _commandBus.Send<UpdateProductCommand, bool>
                                 (new UpdateProductCommand(request));

                if (!result.IsSuccess)
                {
                    var message = result.GetErrorMessage();
                    return BadRequest(message);
                }

                return Ok(MessageHelper.Format(AppMessages.Edit, AppEntity.Product));


            })
                .WithTags(ApiInfo.Tag);
        }
    }
}