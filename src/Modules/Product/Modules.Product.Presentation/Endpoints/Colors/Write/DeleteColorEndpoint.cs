using Framwork.Bus.Command;
using Framwork.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Product.Application.Contract.UseCase.Colors.Commands;
using SharedKernel.Constants;
using SharedKernel.Helper;
using SharedKernel.Interface;

namespace Modules.Product.Presentation.Endpoints.Colors.Write;



public static class DeleteColorEndpoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete($"{ApiInfo.Prefix}/{{id}}", handler: async (

                  long id,
                   
                  [FromServices] ICommandBus _commandBus, CancellationToken ct ) =>
            {


                var result = await _commandBus.Send<DeleteColorCommand, bool>
                                 (new DeleteColorCommand(id), ct);

                if (!result.IsSuccess)
                {
                    var message = result.GetErrorMessage();
                    return BadRequest(message);
                }

                return Ok(MessageHelper.Format(AppMessages.Delete, AppEntity.Color));


            })
                .WithTags(ApiInfo.Tag);
        }
    }
}

