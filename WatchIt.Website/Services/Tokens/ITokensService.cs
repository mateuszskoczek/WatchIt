namespace WatchIt.Website.Services.Tokens;

public interface ITokensService
{
    #region Access token
    
    Task<string?> GetAccessToken();
    Task SetAccessToken(string accessToken);
    Task DeleteAccessToken();
    
    #endregion
    
    #region Refresh token
    
    Task<string?> GetRefreshToken();
    Task SetRefreshToken(string accessToken);
    Task DeleteRefreshToken();
    
    #endregion
}