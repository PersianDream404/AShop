using Framwork.Bus.Command;
using Identity.Application.Contract.DTOs.Users;
using Identity.Application.Contract.Users.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ParsizCRM.API.Features.Account;
using SharedKernel.Interface;

namespace Identity.Presentation.Users.Create;

//public sealed class CreateUserEndpoint : BaseEndpoint, IEndpoint
//{
//    public void MapEndpoint(IEndpointRouteBuilder app)
//    {
//        app.MapPost("/api/users",
//    async (CreateUserRequestDto request,
//           ICommandBus bus)
//        => await Handle(request, bus));
//    }

//    private async Task<IResult> Handle(
//       CreateUserRequestDto request,
//        ICommandBus commandBus)
//    {

//        return Ok( "کاربر با موفقیت ایجاد شد");
//    }
//}

public static class CreateUserEndpoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{ApiInfo.Prefix}/Login", handler: async (


                  [FromBody] CreateUserRequestDto request,
                  [FromServices] ICommandBus _commandBus
                ) =>
            {

                var result = await _commandBus.Send<CreateUserCommand, bool>(
                new CreateUserCommand(request));

                //if (!result.IsSuccess)
                //{
                //    var message = result.GetErrorMessage();
                //}

                return Ok("تست");


            })
                .WithTags(ApiInfo.Tag);
        }
    }
}