using Framwork.Bus.Command;

namespace Identity.Application.Contract.UseCase.Addresses.Commands;

public sealed record DeleteAddressCommand(long UserId, long AddressId) : ICommand<bool>;
