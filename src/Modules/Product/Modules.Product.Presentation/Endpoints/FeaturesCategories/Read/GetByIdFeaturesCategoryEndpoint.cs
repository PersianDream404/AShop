using Framwork.Bus.Query;
using Framwork.Extensions;
using Framwork.PagedList;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using SharedKernel.Interface;
using Modules.Product.Application.Contract.UseCase.FeaturesCategorys.Queries;
using Modules.Product.Application.Contract.DTOs.FeaturesCategorys.GetAll;
using Modules.Product.Presentation.Endpoints.FeaturesCategorys;
using Modules.Product.Presentation.Endpoints.FeaturesCategory;

namespace Modules.FeaturesCategory.Presentation.Endpoints.FeaturesCategorys.Write;

public static class GetByIdFeaturesCategoryEndpoint
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

                var result = await _queryBus.Send<GetByIdFeaturesCategoryQuery, GetByIdFeaturesCategoryResponseDto>
                                 (new GetByIdFeaturesCategoryQuery(id));

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