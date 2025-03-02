using System.Security.Cryptography;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.Extensions.Caching.Memory;

namespace WatchIt.Website.Services.Tokens;

public class TokensService : ITokensService
{
    #region SERVICES

    private readonly ILogger<TokensService> _logger;
    private readonly ProtectedLocalStorage _localStorageService;
    private readonly Configuration.Tokens _configuration;

    #endregion
    
    
    
    #region CONSTRUCTORS

    public TokensService(ILogger<TokensService> logger, IConfiguration configuration, ProtectedLocalStorage localStorageService)
    {
        _logger = logger;
        _localStorageService = localStorageService;
        _configuration = configuration.GetSection("Tokens").Get<Configuration.Tokens>()!;
    }

    #endregion



    #region PUBLIC METHODS

    #region Access token
    
    public async Task<string?> GetAccessToken() => await GetValueAsync<string>(_configuration.StorageKeys.AccessToken);
    
    public async Task SetAccessToken(string accessToken) => await _localStorageService.SetAsync(_configuration.StorageKeys.AccessToken, accessToken);

    public async Task DeleteAccessToken() => await _localStorageService.DeleteAsync(_configuration.StorageKeys.AccessToken);
    
    #endregion

    #region Refresh token

    public async Task<string?> GetRefreshToken() => await GetValueAsync<string>(_configuration.StorageKeys.RefreshToken);
    
    public async Task SetRefreshToken(string accessToken) => await _localStorageService.SetAsync(_configuration.StorageKeys.RefreshToken, accessToken);

    public async Task DeleteRefreshToken() => await _localStorageService.DeleteAsync(_configuration.StorageKeys.RefreshToken);
    
    #endregion
    
    #endregion



    #region PRIVATE METHODS

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