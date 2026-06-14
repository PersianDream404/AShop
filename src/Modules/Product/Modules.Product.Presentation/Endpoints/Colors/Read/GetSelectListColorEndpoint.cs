using Framwork.Bus.Query;
using Framwork.Extensions;
using Framwork.PagedList;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using SharedKernel.Interface;
using Modules.Product.Application.Contract.UseCase.Colors.Queries;
using Modules.Product.Presentation.Endpoints.Colors;
using Modules.Product.Application.Contract.DTOs.Colors.GetAll;


namespace Modules.Color.Presentation.Endpoints.Colors.Write;



public static class GetSelectListColorEndpoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/SelectList", handler: async (


                    [AsParameters] GetSelectListColorRequestDto request,
                  [FromServices] IQueryBus _queryBus
                ) =>
            {

                var result = await _queryBus.Send<GetSelectListColorQuery, PagedList< GetSelectListColorResponseDto >>
                                 (new GetSelectListColorQuery(request));

                if (!result.IsSuccess)
                {
                    var message = result.GetErrorMessage();
                    return BadRequest(message);
                }

                return Ok(result.Value);


            })
                .WithTags(ApiInfo.Tag);
        }
    }
}
