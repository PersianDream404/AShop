using Framwork.Bus.Query;
using Framwork.Extensions;
using Framwork.PagedList;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using SharedKernel.Interface;
using Modules.Product.Application.Contract.UseCase.FeaturesValuess.Queries;
using Modules.Product.Presentation.Endpoints.FeaturesValuess;
using Modules.Product.Application.Contract.DTOs.FeaturesValuess.GetAll;


namespace Modules.FeaturesValues.Presentation.Endpoints.FeaturesValuess.Write;



public static class GetSelectListFeaturesValuesEndpoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/SelectList", handler: async (


                    [AsParameters] GetSelectListFeaturesValuesRequestDto request,
                  [FromServices] IQueryBus _queryBus
                ) =>
            {

                var result = await _queryBus.Send<GetSelectListFeaturesValuesQuery, PagedList< GetSelectListFeaturesValuesResponseDto >>
                                 (new GetSelectListFeaturesValuesQuery(request));

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
