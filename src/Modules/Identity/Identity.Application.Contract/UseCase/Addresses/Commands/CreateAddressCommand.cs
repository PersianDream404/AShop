using Framwork.Bus.Command;
using Identity.Application.Contract.DTOs.Addresses;

namespace Identity.Application.Contract.UseCase.Addresses.Commands;

public sealed record CreateAddressCommand(long UserId, AddressRequestDto Request) : ICommand<AddressDto>;
