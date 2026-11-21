using Identity.Application.Contract.DTOs.Addresses;
using Identity.Domain.Entities;

namespace Identity.Application.Addresses;

internal static class AddressDtoMapper
{
    public static AddressDto Map(Address address) => new()
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
