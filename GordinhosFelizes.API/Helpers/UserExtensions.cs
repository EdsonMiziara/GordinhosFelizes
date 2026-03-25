using System.Security.Claims;

namespace GordinhosFelizes.API.Helpers;

public static class UserExtensions
{
    public static int GetUserId(this ClaimsPrincipal user)
    {
        return int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)!.Value);
    }
}
