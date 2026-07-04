using Framwork.Bus.Query;
using Framwork.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Order.Application.Contract.DTOs;
using Modules.Order.Application.Contract.UseCase.ShoppingCarts.Queries;
using SharedKernel.Interface;

namespace Modules.Order.Presentation.Endpoints.ShoppingCarts.Read;

public static class GetShoppingCartByIdEndpoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/{{cartId}}", handler: async (
                    long cartId,
                    [FromServices] IQueryBus queryBus, CancellationToken ct
                ) =>
            {
                var result = await queryBus.Send<GetShoppingCartByIdQuery, ShoppingCartDto>(
                    new GetShoppingCartByIdQuery(cartId), ct);

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
