using Framwork.Bus.Query;
using Framwork.Extensions;
using Framwork.PagedList;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using SharedKernel.Interface;
using Modules.Product.Application.Contract.DTOs.FeaturesCategorys.GetAll;
using Modules.Product.Application.Contract.UseCase.FeaturesCategorys.Queries;
using Modules.Product.Presentation.Endpoints.FeaturesCategorys;


namespace Modules.FeaturesCategory.Presentation.Endpoints.FeaturesCategorys.Write;

using Modules.Product.Presentation.Endpoints.FeaturesCategory;


public static class GetAllFeaturesCategoryEndpoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}", handler: async (


                    [AsParameters] GetAllFeaturesCategoryRequestDto request,
                  [FromServices] IQueryBus _queryBus
                ) =>
            {

                var result = await _queryBus.Send<GetAllFeaturesCategoryQuery, PagedList< GetAllFeaturesCategoryResponseDto >>
                                 (new GetAllFeaturesCategoryQuery(request));

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
