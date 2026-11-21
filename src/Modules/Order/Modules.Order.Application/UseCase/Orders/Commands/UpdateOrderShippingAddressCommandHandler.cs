using Ardalis.Result;
using Framwork.Bus.Command;
using Framwork.Bus.Query;
using Identity.Application.Contract.DTOs.Addresses;
using Identity.Application.Contract.UseCase.Addresses.Queries;
using Modules.Order.Application.Contract.Interface.Orders;
using Modules.Order.Application.Contract.UseCase.Orders.Commands;
using Modules.Order.Domain.Interfaces;

namespace Modules.Order.Application.UseCase.Orders.Commands;

public sealed class UpdateOrderShippingAddressCommandHandler(
    IOrderCommandRepository commandRepository,
    IQueryBus queryBus)
    : ICommandHandler<UpdateOrderShippingAddressCommand, bool>
{
    public async Task<Result<bool>> Handle(
        UpdateOrderShippingAddressCommand command,
        CancellationToken cancellationToken)
    {
        var order = await commandRepository.GetByIdForUserAsync(
            command.OrderId,
            command.UserId,
            cancellationToken);

        if (order is null)
            return Result.NotFound("Order not found or is not editable");

        var addressResult = await queryBus.Send<GetAddressByIdQuery, AddressDto>(
            new GetAddressByIdQuery(command.UserId, command.AddressId),
            cancellationToken);

        if (!addressResult.IsSuccess)
        {
            if (addressResult.Status == ResultStatus.NotFound)
                return Result.NotFound("Address not found");

            if (addressResult.Status == ResultStatus.Invalid)
                return Result.Invalid(addressResult.ValidationErrors);

            return Result.Error(addressResult.Errors.FirstOrDefault() ?? "Address lookup failed");
        }

        var updateResult = order.UpdateShippingAddress(
            addressResult.Value.Id,
            addressResult.Value.RecipientName,
            addressResult.Value.Province,
            addressResult.Value.City,
            addressResult.Value.PostalCode,
            addressResult.Value.AddressLine,
            addressResult.Value.MobileNumber);

        if (!updateResult.IsSuccess)
            return Result.Invalid(updateResult.ValidationErrors);

        await commandRepository.UpdateAsync(order, cancellationToken);
        return Result.Success(true);
    }
}
