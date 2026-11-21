using Ardalis.Result;
using Framwork.Bus.Query;
using Identity.Application.Contract.DTOs.Addresses;
using Identity.Application.Contract.Interface.Addresses;
using Identity.Application.Contract.UseCase.Addresses.Queries;

namespace Identity.Application.Addresses.Queries;

public sealed class GetAddressByIdQueryHandler(IAddressQueryRepository queryRepository)
    : IQueryHandler<GetAddressByIdQuery, AddressDto>
{
    public async Task<Result<AddressDto>> Handle(
        GetAddressByIdQuery query,
        CancellationToken cancellationToken)
    {
        var address = await queryRepository.GetByIdAsync(
            query.AddressId,
            query.UserId,
            cancellationToken);

        return address is null
            ? Result.NotFound("Address not found")
            : Result.Success(AddressDtoMapper.Map(address));
    }
}
