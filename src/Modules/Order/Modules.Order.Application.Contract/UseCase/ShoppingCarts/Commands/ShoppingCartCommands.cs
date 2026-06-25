using Framwork.Bus.Command;
using Modules.Order.Application.Contract.DTOs;

namespace Modules.Order.Application.Contract.UseCase.ShoppingCarts.Commands;

public record CreateShoppingCartCommand(CreateShoppingCartRequestDto Request) : ICommand<long>;
public record LinkSessionToUserCommand(LinkSessionToUserRequestDto Request) : ICommand<bool>;
