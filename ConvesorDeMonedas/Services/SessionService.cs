using ConvesorDeMonedas.Models;
using System.Security.Claims;

namespace ConvesorDeMonedas.Services;

public class SessionService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public SessionService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int GetUserId()
    {
        Claim? userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst("userId");

        if (userIdClaim == null)
        {
            throw new Exception("No se pudo validar la sesión.");
        }
        else
        {
            return int.Parse(userIdClaim.Value);
        }
    }

    public Role GetUserRol()
    {
        Claim? userRoleClaim = _httpContextAccessor.HttpContext.User.FindFirst("http://schemas.microsoft.com/ws/2008/06/identity/claims/role");
        var userClaims = _httpContextAccessor.HttpContext.User.Claims;

        if (userRoleClaim == null)
        {
            throw new Exception("No se pudo validar el rol del usuario.");
        }
        else
        {
            return (Role)Enum.Parse(typeof(Role), userRoleClaim.Value);
        }
    }
}