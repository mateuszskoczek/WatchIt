using System.Security.Cryptography;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.Extensions.Logging;
using WatchIt.Common.Model.Accounts;
using WatchIt.Website.Services.Utility.Configuration;

namespace WatchIt.Website.Services.Utility.Tokens;

public class TokensService : ITokensService
{
    #region SERVICES

    private readonly ILogger<TokensService> _logger;
    
    private readonly ProtectedLocalStorage _localStorageService;
    private readonly IConfigurationService _configurationService;

    #endregion
    
    
    
    #region CONSTRUCTORS

    public TokensService(ILogger<TokensService> logger, ProtectedLocalStorage localStorageService, IConfigurationService configurationService)
    {
        _logger = logger;
        
        _localStorageService = localStorageService;
        _configurationService = configurationService;
    }

    #endregion



    #region PUBLIC METHODS

    public async Task<string?> GetAccessToken() => await GetValueAsync<string>(GetAccessTokenStorageKey());

    public async Task<string?> GetRefreshToken() => await GetValueAsync<string>(GetRefreshTokenStorageKey());

    public async Task SaveAuthenticationData(AuthenticateResponse authenticateResponse) => await Task.WhenAll(SaveAccessToken(authenticateResponse.AccessToken), SaveRefreshToken(authenticateResponse.RefreshToken));

    public async Task SaveAccessToken(string accessToken) => await _localStorageService.SetAsync(GetAccessTokenStorageKey(), accessToken);

    public async Task SaveRefreshToken(string refreshToken) => await _localStorageService.SetAsync(GetRefreshTokenStorageKey(), refreshToken);

    public async Task RemoveAuthenticationData() => await Task.WhenAll(RemoveAccessToken(), RemoveRefreshToken());

    public async Task RemoveAccessToken() => await _localStorageService.DeleteAsync(GetAccessTokenStorageKey());

    public async Task RemoveRefreshToken() => await _localStorageService.DeleteAsync(GetRefreshTokenStorageKey());

    #endregion
    
    
    
    #region PRIVATE METHODS

    private string GetAccessTokenStorageKey() => _configurationService.Data.StorageKeys.AccessToken;
    private string GetRefreshTokenStorageKey() => _configurationService.Data.StorageKeys.RefreshToken;

    private async Task<T?> GetValueAsync<T>(string key)
    {
        try
        {
            ProtectedBrowserStorageResult<T> result = await _localStorageService.GetAsync<T>(key);
            return result.Success ? result.Value : default;
        }
        catch (CryptographicException ex)
        {
            _logger.LogError(ex, "Browser storage error has occurred. Deleting value.");
            await _localStorageService.DeleteAsync(key);
            return default;
        }
    }

    #endregion
}