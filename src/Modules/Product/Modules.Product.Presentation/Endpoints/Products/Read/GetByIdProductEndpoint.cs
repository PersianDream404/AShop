using Framwork.Bus.Query;
using Framwork.Extensions;
using Framwork.PagedList;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using SharedKernel.Interface;
using Modules.Product.Application.Contract.UseCase.Products.Queries;
using Modules.Product.Application.Contract.DTOs.Products.GetAll;
using Modules.Product.Presentation.Endpoints.Products;
using Modules.Product.Application.Contract.DTOs.Products.Get;


namespace Modules.Product.Presentation.Endpoints.Products.Write;

public static class GetByIdProductEndpoint
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

                var result = await _queryBus.Send<GetByIdProductQuery, GetByIdProductResponseDto>
                                 (new GetByIdProductQuery(id));

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