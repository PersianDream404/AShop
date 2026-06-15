using Framwork.Bus.Query;
using Framwork.Extensions;
using Framwork.PagedList;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using SharedKernel.Interface;
using Modules.Product.Application.Contract.DTOs.Categorys.GetAll;
using Modules.Product.Application.Contract.UseCase.Categorys.Queries;
using Modules.Product.Presentation.Endpoints.Categorys;


namespace Modules.Category.Presentation.Endpoints.Categorys.Write;



public static class GetAllCategoryEndpoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}", handler: async (


                    [AsParameters] GetAllCategoryRequestDto request,
                  [FromServices] IQueryBus _queryBus
                ) =>
            {

                var result = await _queryBus.Send<GetAllCategoryQuery, PagedList< GetAllCategoryResponseDto >>
                                 (new GetAllCategoryQuery(request));

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
