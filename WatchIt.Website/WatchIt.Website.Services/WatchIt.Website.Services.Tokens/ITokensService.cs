using WatchIt.Common.Model.Accounts;

namespace WatchIt.Website.Services.Tokens;

public interface ITokensService
{
    Task<string?> GetAccessToken();
    Task<string?> GetRefreshToken();
    Task SaveAuthenticationData(AuthenticateResponse authenticateResponse);
    Task SaveAccessToken(string accessToken);
    Task SaveRefreshToken(string refreshToken);
    Task RemoveAuthenticationData();
    Task RemoveAccessToken();
    Task RemoveRefreshToken();
}