using Framwork.Bus.Command;
using Framwork.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Product.Application.Contract.UseCase.Brands.Commands;
using SharedKernel.Constants;
using SharedKernel.Helper;
using SharedKernel.Interface;

namespace Modules.Product.Presentation.Endpoints.Brands.Write;


public static class DeleteBrandEndpoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete($"{ApiInfo.Prefix}/{{id}}", handler: async (

                  long id,
                   
                  [FromServices] ICommandBus _commandBus, CancellationToken ct
                ) =>
            {


                var result = await _commandBus.Send<DeleteBrandCommand, bool>
                                 (new DeleteBrandCommand(id), ct);

                if (!result.IsSuccess)
                {
                    var message = result.GetErrorMessage();
                    return BadRequest(message);
                }

                return Ok(MessageHelper.Format(AppMessages.Delete, AppEntity.Brand));


            })
                .WithTags(ApiInfo.Tag);
        }
    }
}
