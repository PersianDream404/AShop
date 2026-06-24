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

public static class GetShoppingCartByUserIdEndpoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/user/{{userId}}", handler: async (
                    long userId,
                    [FromServices] IQueryBus queryBus
                ) =>
            {
                var result = await queryBus.Send<GetShoppingCartByUserIdQuery, ShoppingCartDto>(
                    new GetShoppingCartByUserIdQuery(userId));

                if (!result.IsSuccess)
                {
                    var message = result.GetErrorMessage();
                    return BadRequest(message);
                }

                return Ok(result.Value);
            })
                .WithTags(ApiInfo.Prefix);
        }
    }
}
