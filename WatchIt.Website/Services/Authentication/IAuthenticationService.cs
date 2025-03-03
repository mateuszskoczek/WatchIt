using Refit;
using WatchIt.DTO.Models.Controllers.Authentication;

namespace WatchIt.Website.Services.Authentication;

public interface IAuthenticationService
{
    Task<string?> GetRawAccessTokenAsync();
    Task<long?> GetAccountIdAsync();
    Task<IApiResponse> Login(AuthenticationRequest data);
    Task<IApiResponse> Logout();
}