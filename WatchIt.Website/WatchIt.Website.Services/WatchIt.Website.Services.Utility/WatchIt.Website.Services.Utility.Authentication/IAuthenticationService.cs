namespace WatchIt.Website.Services.Utility.Authentication;

public interface IAuthenticationService
{
    Task<User?> GetUserAsync();
    Task<bool> GetAuthenticationStatusAsync();
    Task LogoutAsync();
}