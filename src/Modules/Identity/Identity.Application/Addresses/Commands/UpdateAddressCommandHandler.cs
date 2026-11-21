using Ardalis.Result;
using Framwork.Bus.Command;
using Identity.Application.Contract.Interface.Addresses;
using Identity.Application.Contract.UseCase.Addresses.Commands;
using Identity.Domain.Interface;

namespace Identity.Application.Addresses.Commands;

public sealed class UpdateAddressCommandHandler(
    IAddressCommandRepository commandRepository,
    IAddressQueryRepository queryRepository) : ICommandHandler<UpdateAddressCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateAddressCommand command, CancellationToken cancellationToken)
    {
        var address = await queryRepository.GetByIdAsync(command.AddressId, command.UserId, cancellationToken);
        if (address is null)
            return Result.NotFound("Address not found");

        address.Update(
            command.Request.Title,
            command.Request.RecipientName,
            command.Request.Province,
            command.Request.City,
            command.Request.PostalCode,
            command.Request.AddressLine,
            command.Request.MobileNumber);

        if (command.Request.IsDefault)
        {
            var addresses = await queryRepository.GetByUserIdAsync(command.UserId, cancellationToken);
            foreach (var item in addresses.Where(x => x.Id != address.Id))
            {
                item.SetDefault(false);
                await commandRepository.UpdateAsync(item, cancellationToken);
            }

            address.SetDefault(true);
        }

        await commandRepository.UpdateAsync(address, cancellationToken);
        return Result.Success(true);
    }
}
