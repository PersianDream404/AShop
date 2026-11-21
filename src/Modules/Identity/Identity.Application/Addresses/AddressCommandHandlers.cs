using Ardalis.Result;
using FluentValidation;
using Framwork.Bus.Command;
using Framwork.Bus.Query;
using Identity.Application.Contract.DTOs.Addresses;
using Identity.Application.Contract.UseCase.Addresses;
using Identity.Domain.Entities;
using Identity.Domain.Interface;

namespace Identity.Application.Addresses;

public sealed class CreateAddressCommandHandler(
    IAddressCommandRepository commandRepository,
    IAddressQueryRepository queryRepository) : ICommandHandler<CreateAddressCommand, AddressDto>
{
    public async Task<Result<AddressDto>> Handle(CreateAddressCommand command, CancellationToken cancellationToken)
    {
        if (command.Request.IsDefault)
        {
            var addresses = await queryRepository.GetByUserIdAsync(command.UserId, cancellationToken);
            foreach (var address in addresses)
                address.SetDefault(false);
        }

        var existing = await queryRepository.GetByUserIdAsync(command.UserId, cancellationToken);
        var addressToCreate = new Address(
            command.UserId,
            command.Request.Title,
            command.Request.RecipientName,
            command.Request.Province,
            command.Request.City,
            command.Request.PostalCode,
            command.Request.AddressLine,
            command.Request.MobileNumber,
            command.Request.IsDefault || existing.Count == 0);

        if (command.Request.IsDefault)
        {
            foreach (var address in existing)
                await commandRepository.UpdateAsync(address, cancellationToken);
        }

        var created = await commandRepository.AddAsync(addressToCreate, cancellationToken);
        return Result.Success(Map(created));
    }

    internal static AddressDto Map(Address address) => new()
    {
        Id = address.Id,
        Title = address.Title,
        RecipientName = address.RecipientName,
        Province = address.Province,
        City = address.City,
        PostalCode = address.PostalCode,
        AddressLine = address.AddressLine,
        MobileNumber = address.MobileNumber,
        IsDefault = address.IsDefault
    };
}

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

public sealed class DeleteAddressCommandHandler(
    IAddressCommandRepository commandRepository,
    IAddressQueryRepository queryRepository) : ICommandHandler<DeleteAddressCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteAddressCommand command, CancellationToken cancellationToken)
    {
        var address = await queryRepository.GetByIdAsync(command.AddressId, command.UserId, cancellationToken);
        if (address is null)
            return Result.NotFound("Address not found");

        address.SoftDelete();
        await commandRepository.UpdateAsync(address, cancellationToken);

        if (address.IsDefault)
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

public sealed class GetMyAddressesQueryHandler(IAddressQueryRepository queryRepository)
    : IQueryHandler<GetMyAddressesQuery, IReadOnlyCollection<AddressDto>>
{
    public async Task<Result<IReadOnlyCollection<AddressDto>>> Handle(
        GetMyAddressesQuery query,
        CancellationToken cancellationToken)
    {
        var addresses = await queryRepository.GetByUserIdAsync(query.UserId, cancellationToken);
        return Result.Success<IReadOnlyCollection<AddressDto>>(addresses.Select(CreateAddressCommandHandler.Map).ToArray());
    }
}

public sealed class AddressRequestValidator : AbstractValidator<AddressRequestDto>
{
    public AddressRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
        RuleFor(x => x.RecipientName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Province).NotEmpty().MaximumLength(100);
        RuleFor(x => x.City).NotEmpty().MaximumLength(100);
        RuleFor(x => x.PostalCode).NotEmpty().MaximumLength(20);
        RuleFor(x => x.AddressLine).NotEmpty().MaximumLength(500);
        RuleFor(x => x.MobileNumber).NotEmpty().MaximumLength(20);
    }
}

public sealed class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
{
    public CreateAddressCommandValidator() => RuleFor(x => x.Request).SetValidator(new AddressRequestValidator());
}

public sealed class UpdateAddressCommandValidator : AbstractValidator<UpdateAddressCommand>
{
    public UpdateAddressCommandValidator() => RuleFor(x => x.Request).SetValidator(new AddressRequestValidator());
}
