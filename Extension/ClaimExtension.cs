using System.Security.Claims;

namespace Server.Extension;

public static class ClaimsExtensions
{
    public static string GetUsername(this ClaimsPrincipal user)
    {
        return user.Claims.SingleOrDefault(x=>x.Type==ClaimTypes.GivenName)?.Value;
    }
}