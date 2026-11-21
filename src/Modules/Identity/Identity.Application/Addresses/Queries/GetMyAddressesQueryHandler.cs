using Ardalis.Result;
using Framwork.Bus.Query;
using Identity.Application.Contract.DTOs.Addresses;
using Identity.Application.Contract.Interface.Addresses;
using Identity.Application.Contract.UseCase.Addresses.Queries;

namespace Identity.Application.Addresses.Queries;

public sealed class GetMyAddressesQueryHandler(IAddressQueryRepository queryRepository)
    : IQueryHandler<GetMyAddressesQuery, IReadOnlyCollection<AddressDto>>
{
    public async Task<Result<IReadOnlyCollection<AddressDto>>> Handle(
        GetMyAddressesQuery query,
        CancellationToken cancellationToken)
    {
        var addresses = await queryRepository.GetByUserIdAsync(query.UserId, cancellationToken);
        return Result.Success<IReadOnlyCollection<AddressDto>>(addresses.Select(AddressDtoMapper.Map).ToArray());
    }
}
