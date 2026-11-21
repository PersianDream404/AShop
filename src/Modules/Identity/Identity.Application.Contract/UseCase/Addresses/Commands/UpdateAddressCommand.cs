using Framwork.Bus.Command;
using Identity.Application.Contract.DTOs.Addresses;

namespace Identity.Application.Contract.UseCase.Addresses.Commands;

public sealed record UpdateAddressCommand(long UserId, long AddressId, AddressRequestDto Request) : ICommand<bool>;
