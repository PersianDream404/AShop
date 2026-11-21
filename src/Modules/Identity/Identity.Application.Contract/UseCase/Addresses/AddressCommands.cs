using Framwork.Bus.Command;
using Framwork.Bus.Query;
using Identity.Application.Contract.DTOs.Addresses;

namespace Identity.Application.Contract.UseCase.Addresses;

public sealed record CreateAddressCommand(long UserId, AddressRequestDto Request) : ICommand<AddressDto>;
public sealed record UpdateAddressCommand(long UserId, long AddressId, AddressRequestDto Request) : ICommand<bool>;
public sealed record DeleteAddressCommand(long UserId, long AddressId) : ICommand<bool>;
public sealed record SetDefaultAddressCommand(long UserId, long AddressId) : ICommand<bool>;
public sealed record GetMyAddressesQuery(long UserId) : IQuery<IReadOnlyCollection<AddressDto>>;
