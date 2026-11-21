using Ardalis.Result;
using Framwork.Bus.Command;
using Identity.Application.Contract.Interface.Addresses;
using Identity.Application.Contract.UseCase.Addresses.Commands;
using Identity.Domain.Interface;

namespace Identity.Application.Addresses.Commands;

public sealed class SetDefaultAddressCommandHandler(
    IAddressCommandRepository commandRepository,
    IAddressQueryRepository queryRepository) : ICommandHandler<SetDefaultAddressCommand, bool>
{
    public async Task<Result<bool>> Handle(SetDefaultAddressCommand command, CancellationToken cancellationToken)
    {
        var address = await queryRepository.GetByIdAsync(command.AddressId, command.UserId, cancellationToken);
        if (address is null)
            return Result.NotFound("Address not found");

        var addresses = await queryRepository.GetByUserIdAsync(command.UserId, cancellationToken);
        foreach (var item in addresses)
        {
            item.SetDefault(item.Id == address.Id);
            await commandRepository.UpdateAsync(item, cancellationToken);
        }

        return Result.Success(true);
    }
}
