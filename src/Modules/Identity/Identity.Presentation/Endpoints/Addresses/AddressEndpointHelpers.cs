using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Identity.Presentation.Endpoints.Addresses;

internal static class AddressEndpointHelpers
{
    public static bool TryGetUserId(HttpContext httpContext, out long userId)
    {
        var value = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)
                    ?? httpContext.User.FindFirstValue("UserId");

        return long.TryParse(value, out userId);
    }
}
