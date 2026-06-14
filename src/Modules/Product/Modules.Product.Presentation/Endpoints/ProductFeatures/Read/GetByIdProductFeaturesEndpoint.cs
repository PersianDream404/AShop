using Framwork.Bus.Query;
using Framwork.Extensions;
using Framwork.PagedList;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using SharedKernel.Interface;
using Modules.Product.Application.Contract.UseCase.ProductFeaturess.Queries;
using Modules.Product.Application.Contract.DTOs.ProductFeaturess.GetAll;
using Modules.Product.Presentation.Endpoints.ProductFeaturess;


namespace Modules.ProductFeatures.Presentation.Endpoints.ProductFeaturess.Write;

public static class GetByIdProductFeaturesEndpoint
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

                var result = await _queryBus.Send<GetByIdProductFeaturesQuery, GetByIdProductFeaturesResponseDto>
                                 (new GetByIdProductFeaturesQuery(id));

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