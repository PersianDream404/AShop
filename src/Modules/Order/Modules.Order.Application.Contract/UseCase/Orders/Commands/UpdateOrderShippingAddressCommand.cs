using Framwork.Bus.Command;

namespace Modules.Order.Application.Contract.UseCase.Orders.Commands;

public sealed record UpdateOrderShippingAddressCommand(long UserId, long OrderId, long AddressId)
    : ICommand<bool>;
