using WatchIt.Database.Model.Accounts;

namespace WatchIt.WebAPI.Services.Tokens;

public interface ITokensService
{
    string CreateAccessToken(Account account);
    Task<string> CreateRefreshTokenAsync(Account account, bool isExtendable);
    Task<Account> ExtendRefreshTokenAsync(string refreshToken, string accessToken);
    Task RevokeRefreshTokenAsync(string stringToken);
    Task RevokeRefreshTokenAsync(Guid token);
    Task RevokeAccountRefreshTokensAsync(Account account);
}