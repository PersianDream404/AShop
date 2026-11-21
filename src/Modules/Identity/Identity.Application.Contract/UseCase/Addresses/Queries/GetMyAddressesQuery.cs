using Framwork.Bus.Query;
using Identity.Application.Contract.DTOs.Addresses;

namespace Identity.Application.Contract.UseCase.Addresses.Queries;

public sealed record GetMyAddressesQuery(long UserId) : IQuery<IReadOnlyCollection<AddressDto>>;
