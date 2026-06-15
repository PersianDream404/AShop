using Framwork.Bus.Query;
using Framwork.Extensions;
using Framwork.PagedList;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using SharedKernel.Interface;
using Modules.Product.Application.Contract.UseCase.FeaturesValuess.Queries;
using Modules.Product.Application.Contract.DTOs.FeaturesValuess.GetAll;
using Modules.Product.Presentation.Endpoints.FeaturesValuess;


namespace Modules.FeaturesValues.Presentation.Endpoints.FeaturesValuess.Write;

public static class GetByIdFeaturesValuesEndpoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/{{id}}", handler: async (


                    int id,
                  [FromServices] IQueryBus _queryBus
                ) =>
            {

                var result = await _queryBus.Send<GetByIdFeaturesValuesQuery, GetByIdFeaturesValuesResponseDto>
                                 (new GetByIdFeaturesValuesQuery(id));

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