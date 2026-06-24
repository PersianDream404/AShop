namespace Modules.Order.Application.Contract.DTOs;

public class ShoppingCartDto
{
    public long Id { get; set; }
    public long SessionId { get; set; }
    public long? UserId { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateShoppingCartRequestDto
{
    public long SessionId { get; set; }
    public long? UserId { get; set; }
}

public class LinkSessionToUserRequestDto
{
    public long CartId { get; set; }
    public long UserId { get; set; }
}
