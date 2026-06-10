using Framwork.Bus.Command;
using Framwork.Bus.Query;
using Framwork.Extensions;
using Framwork.PagedList;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Product.Application.Contract.DTOs.Products;
using Modules.Product.Application.Contract.DTOs.Products.Create;
using Modules.Product.Application.Contract.UseCase.Products.Commands;
using Modules.Product.Application.Contract.UseCase.Products.Queries;
using ParsizCRM.API.Features.Account;
using SharedKernel.Constants;
using SharedKernel.Interface;

namespace Modules.Product.Presentation.Endpoints.Products.Write;



public static class GetAllProductEndpoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}", handler: async (


                    [AsParameters] GetAllProductRequestDto request,
                  [FromServices] IQueryBus _queryBus
                ) =>
            {

                var result = await _queryBus.Send<GetAllProductQuery, PagedList< GetAllProductResponseDto >>
                                 (new GetAllProductQuery(request));

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