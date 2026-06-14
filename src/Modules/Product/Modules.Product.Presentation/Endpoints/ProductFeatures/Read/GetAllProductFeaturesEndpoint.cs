using Framwork.Bus.Query;
using Framwork.Extensions;
using Framwork.PagedList;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using SharedKernel.Interface;
using Modules.Product.Application.Contract.DTOs.ProductFeaturess.GetAll;
using Modules.Product.Application.Contract.UseCase.ProductFeaturess.Queries;
using Modules.Product.Presentation.Endpoints.ProductFeaturess;


namespace Modules.ProductFeatures.Presentation.Endpoints.ProductFeaturess.Write;



public static class GetAllProductFeaturesEndpoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}", handler: async (


                    [AsParameters] GetAllProductFeaturesRequestDto request,
                  [FromServices] IQueryBus _queryBus
                ) =>
            {

                var result = await _queryBus.Send<GetAllProductFeaturesQuery, PagedList< GetAllProductFeaturesResponseDto >>
                                 (new GetAllProductFeaturesQuery(request));

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
