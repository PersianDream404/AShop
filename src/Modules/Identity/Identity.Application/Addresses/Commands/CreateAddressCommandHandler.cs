using Ardalis.Result;
using Framwork.Bus.Command;
using Identity.Application.Contract.DTOs.Addresses;
using Identity.Application.Contract.Interface.Addresses;
using Identity.Application.Contract.UseCase.Addresses.Commands;
using Identity.Domain.Entities;
using Identity.Domain.Interface;

namespace Identity.Application.Addresses.Commands;

public sealed class CreateAddressCommandHandler(
    IAddressCommandRepository commandRepository,
    IAddressQueryRepository queryRepository) : ICommandHandler<CreateAddressCommand, AddressDto>
{
    public async Task<Result<AddressDto>> Handle(CreateAddressCommand command, CancellationToken cancellationToken)
    {
        var existing = await queryRepository.GetByUserIdAsync(command.UserId, cancellationToken);
        var makeDefault = command.Request.IsDefault || existing.Count == 0;

        if (makeDefault)
        {
            foreach (var address in existing)
            {
                address.SetDefault(false);
                await commandRepository.UpdateAsync(address, cancellationToken);
            }
        }

        var addressToCreate = new Address(
            command.UserId,
            command.Request.Title,
            command.Request.RecipientName,
            command.Request.Province,
            command.Request.City,
            command.Request.PostalCode,
            command.Request.AddressLine,
            command.Request.MobileNumber,
            makeDefault);

        var created = await commandRepository.AddAsync(addressToCreate, cancellationToken);
        return Result.Success(AddressDtoMapper.Map(created));
    }
}
