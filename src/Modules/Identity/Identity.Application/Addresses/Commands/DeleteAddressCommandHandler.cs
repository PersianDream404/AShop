using Ardalis.Result;
using Framwork.Bus.Command;
using Identity.Application.Contract.Interface.Addresses;
using Identity.Application.Contract.UseCase.Addresses.Commands;
using Identity.Domain.Interface;

namespace Identity.Application.Addresses.Commands;

public sealed class DeleteAddressCommandHandler(
    IAddressCommandRepository commandRepository,
    IAddressQueryRepository queryRepository) : ICommandHandler<DeleteAddressCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteAddressCommand command, CancellationToken cancellationToken)
    {
        var address = await queryRepository.GetByIdAsync(command.AddressId, command.UserId, cancellationToken);
        if (address is null)
            return Result.NotFound("Address not found");

        var wasDefault = address.IsDefault;
        address.SoftDelete();
        await commandRepository.UpdateAsync(address, cancellationToken);

        if (wasDefault)
        {
            var remaining = await queryRepository.GetByUserIdAsync(command.UserId, cancellationToken);
            var nextDefault = remaining.FirstOrDefault();
            if (nextDefault is not null)
            {
                nextDefault.SetDefault(true);
                await commandRepository.UpdateAsync(nextDefault, cancellationToken);
            }
        }

        return Result.Success(true);
    }
}
