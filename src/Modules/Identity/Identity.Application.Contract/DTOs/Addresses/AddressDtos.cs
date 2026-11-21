namespace Identity.Application.Contract.DTOs.Addresses;

public sealed class AddressRequestDto
{
    public string Title { get; set; } = null!;
    public string RecipientName { get; set; } = null!;
    public string Province { get; set; } = null!;
    public string City { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string AddressLine { get; set; } = null!;
    public string MobileNumber { get; set; } = null!;
    public bool IsDefault { get; set; }
}

public sealed class AddressDto
{
    public long Id { get; set; }
    public string Title { get; set; } = null!;
    public string RecipientName { get; set; } = null!;
    public string Province { get; set; } = null!;
    public string City { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string AddressLine { get; set; } = null!;
    public string MobileNumber { get; set; } = null!;
    public bool IsDefault { get; set; }
}
