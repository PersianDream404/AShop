using Framwork.Bus.Command;

namespace Identity.Application.Contract.UseCase.Addresses.Commands;

public sealed record SetDefaultAddressCommand(long UserId, long AddressId) : ICommand<bool>;
