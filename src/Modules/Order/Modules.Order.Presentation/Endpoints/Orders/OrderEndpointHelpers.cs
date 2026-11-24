using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Modules.Order.Presentation.Endpoints.Orders;

internal static class OrderEndpointHelpers
{
    public static bool TryGetUserId(HttpContext httpContext, out long userId)
    {
        var value = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)
                    ?? httpContext.User.FindFirstValue("UserId");

        return long.TryParse(value, out userId);
    }
}
