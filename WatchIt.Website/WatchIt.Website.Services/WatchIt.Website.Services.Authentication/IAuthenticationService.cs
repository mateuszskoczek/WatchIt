namespace WatchIt.Website.Services.Authentication;

public interface IAuthenticationService
{
    Task<User?> GetUserAsync();
    Task<bool> GetAuthenticationStatusAsync();
    Task LogoutAsync();
}