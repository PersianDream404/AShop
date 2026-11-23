namespace Identity.Application.Contract.DTOs.Users;

public class RegisterUserRequestDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string Mobile { get; set; } = null!;
    public string Password { get; set; }=null!;
}

public sealed class UpdateMyProfileRequestDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}

public sealed class UserProfileDto
{
    public long Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Mobile { get; set; }
}

public class RegisterUserResponseDto
{
    public long Id { get; set; }
    public List<string> Roles { get; set; } = [];
}
