using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using WatchIt.Common.Model.Accounts;
using WatchIt.Website.Services.WebAPI.Accounts;

namespace WatchIt.Website.Services.Utility.Tokens;

public class JWTAuthenticationStateProvider : AuthenticationStateProvider
{
    #region SERVICES

    private readonly HttpClient _httpClient;

    private readonly ILogger<JWTAuthenticationStateProvider> _logger;
    
    private readonly ITokensService _tokensService;
    private readonly IAccountsWebAPIService _accountsService;

    #endregion



    #region CONSTRUCTORS

    public JWTAuthenticationStateProvider(HttpClient httpClient, ILogger<JWTAuthenticationStateProvider> logger, ITokensService tokensService, IAccountsWebAPIService accountsService)
    {
        _httpClient = httpClient;

        _logger = logger;
        
        _tokensService = tokensService;
        _accountsService = accountsService;
    }

    #endregion
    
    
    
    #region PUBLIC METHODS
    
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        AuthenticationState state = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        
        Task<string?> accessTokenTask = _tokensService.GetAccessToken();
        Task<string?> refreshTokenTask = _tokensService.GetRefreshToken();
        await Task.WhenAll(accessTokenTask, refreshTokenTask);
        string? accessToken = await accessTokenTask;
        string? refreshToken = await refreshTokenTask;

        bool refreshed = false;

        if (string.IsNullOrWhiteSpace(accessToken))
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                return state;
            }
            
            string? accessTokenNew = await Refresh(refreshToken);

            if (string.IsNullOrWhiteSpace(accessToken))
            {
                return state;
            }

            accessToken = accessTokenNew;
            refreshed = true;
        }
        
        IEnumerable<Claim> claims = GetClaimsFromToken(accessToken);

        Claim? expClaim = claims.FirstOrDefault(c => c.Type == "exp");
        
        if (expClaim is not null && ConvertFromUnixTimestamp(int.Parse(expClaim.Value)) > DateTime.UtcNow)
        {
            if (refreshed)
            {
                return state;
            }
        }
        else
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                return state;
            }

            string? accessTokenNew = await Refresh(refreshToken);

            if (accessTokenNew is null)
            {
                return state;
            }

            accessToken = accessTokenNew;
        }
        
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.Replace("\"", ""));
        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims)));
    }
    
    #endregion



    #region PRIVATE METHODS
    
    private async Task<string?> Refresh(string refreshToken)
    {
        AuthenticateResponse response = null;
        
        await _accountsService.AuthenticateRefresh((data) => response = data);
        
        await _tokensService.SaveAuthenticationData(response);
        
        return response.AccessToken;
    }
    
    private static IEnumerable<Claim> GetClaimsFromToken(string token)
    {
        string payload = token.Split('.')[1];

        switch (payload.Length % 4)
        {
            case 2: payload += "=="; break;
            case 3: payload += "="; break;
        }

        byte[] jsonBytes = Convert.FromBase64String(payload);
        Dictionary<string, object>? keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        if (keyValuePairs is null)
        {
            throw new Exception("Incorrect token");
        }

        return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
    }
    
    public static DateTime ConvertFromUnixTimestamp(int timestamp)
    {
        DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return origin.AddSeconds(timestamp);
    }
    
    #endregion
}