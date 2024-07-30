using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using WatchIt.Database;

namespace WatchIt.WebAPI.Services.Utility.User;

public class UserService(DatabaseContext database, IHttpContextAccessor accessor) : IUserService
{
    #region PUBLIC METHODS

    public ClaimsPrincipal GetRawUser()
    {
        if (accessor.HttpContext is null)
        {
            throw new NullReferenceException();
        }
        return accessor.HttpContext.User;
    }

    public string? GetRawToken()
    {
        if (accessor.HttpContext is null)
        {
            throw new NullReferenceException();
        }
        return accessor.HttpContext.Request.Headers.Authorization;
    }

    public UserValidator GetValidator()
    {
        ClaimsPrincipal rawUser = GetRawUser();
        return new UserValidator(database, rawUser);
    }

    public Guid GetJti()
    {
        ClaimsPrincipal user = GetRawUser();
        Claim jtiClaim = user.FindFirst(JwtRegisteredClaimNames.Jti)!;
        Guid guid = Guid.Parse(jtiClaim.Value);
        return guid;
    }
    
    #endregion
}