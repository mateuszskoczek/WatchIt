using System.Security.Claims;

namespace WatchIt.WebAPI.Services.Utility.User;

public interface IUserService
{
    ClaimsPrincipal GetRawUser();
    string? GetRawToken();
    UserValidator GetValidator();
    Guid GetJti();
    long GetUserId();
}