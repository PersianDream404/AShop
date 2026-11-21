using SharedKernel.Base;

namespace Identity.Domain.Entities;

public sealed class Address : BaseEntityIdentity
{
    private Address()
    {
    }

    public long UserId { get; private set; }
    public string Title { get; private set; } = null!;
    public string RecipientName { get; private set; } = null!;
    public string Province { get; private set; } = null!;
    public string City { get; private set; } = null!;
    public string PostalCode { get; private set; } = null!;
    public string AddressLine { get; private set; } = null!;
    public string MobileNumber { get; private set; } = null!;
    public bool IsDefault { get; private set; }

    public ApplicationUser User { get; private set; } = null!;

    public Address(
        long userId,
        string title,
        string recipientName,
        string province,
        string city,
        string postalCode,
        string addressLine,
        string mobileNumber,
        bool isDefault)
    {
        UserId = userId;
        SetDetails(title, recipientName, province, city, postalCode, addressLine, mobileNumber);
        IsDefault = isDefault;
    }

    public void Update(
        string title,
        string recipientName,
        string province,
        string city,
        string postalCode,
        string addressLine,
        string mobileNumber)
    {
        SetDetails(title, recipientName, province, city, postalCode, addressLine, mobileNumber);
    }

    public void SetDefault(bool isDefault) => IsDefault = isDefault;

    public void SoftDelete() => Deleted = true;

    private void SetDetails(
        string title,
        string recipientName,
        string province,
        string city,
        string postalCode,
        string addressLine,
        string mobileNumber)
    {
        Title = title.Trim();
        RecipientName = recipientName.Trim();
        Province = province.Trim();
        City = city.Trim();
        PostalCode = postalCode.Trim();
        AddressLine = addressLine.Trim();
        MobileNumber = mobileNumber.Trim();
    }
}
