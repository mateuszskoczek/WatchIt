using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using Refit;
using WatchIt.DTO.Models.Controllers.Accounts.Account;
using WatchIt.DTO.Models.Controllers.Accounts.AccountLogout;
using WatchIt.DTO.Models.Controllers.Authentication;
using WatchIt.Website.Clients;
using WatchIt.Website.Services.Tokens;

namespace WatchIt.Website.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    #region FIELDS

    private readonly ITokensService _tokensService;
    private readonly IAuthenticationClient _authenticationClient;
    private readonly IAccountsClient _accountsClient;

    #endregion
    
    

    #region CONSTRUCTORS

    public AuthenticationService(ITokensService tokensService, IAuthenticationClient authenticationClient, IAccountsClient accountsClient)
    {
        _tokensService = tokensService;
        _authenticationClient = authenticationClient;
        _accountsClient = accountsClient;
    }

    #endregion
    
    
    
    #region PUBLIC METHODS

    public async Task<string?> GetRawAccessTokenAsync()
    {
        string? accessToken = await _tokensService.GetAccessToken();
        
        if (string.IsNullOrWhiteSpace(accessToken))
        {
            return null;
        }
        
        if (ValidateToken(accessToken))
        {
            return accessToken;
        }

        string? refreshToken = await _tokensService.GetRefreshToken();
        if (string.IsNullOrWhiteSpace(refreshToken))
        {
            return null;
        }

        IApiResponse<AuthenticationResponse> refreshResponse = await _authenticationClient.AuthenticateRefresh(new AuthenticationRefreshRequest
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        });
        if (refreshResponse.IsSuccessful)
        {
            await UpdateTokens(refreshResponse.Content);
            return refreshResponse.Content.AccessToken;
        }
        return null;
    }

    public async Task<long?> GetAccountIdAsync()
    {
        string? accessToken = await GetRawAccessTokenAsync();
        if (string.IsNullOrWhiteSpace(accessToken))
        {
            return null;
        }

        IEnumerable<Claim> claims = GetClaimsFromToken(accessToken);
        Claim? subClaim = claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);
        if (subClaim is null || !long.TryParse(subClaim.Value, out long accountId))
        {
            return null;
        }
        
        return accountId;
    }

    public async Task<IApiResponse> Login(AuthenticationRequest data)
    {
        IApiResponse<AuthenticationResponse> response = await _authenticationClient.Authenticate(data);
        if (response.IsSuccessful)
        {
            await UpdateTokens(response.Content);
        }
        return response;
    }

    public async Task<IApiResponse> Logout()
    {
        IApiResponse response = await _accountsClient.Logout(new AccountLogoutRequest
        {
            RefreshToken = await _tokensService.GetRefreshToken(),
        });
        if (response.IsSuccessful)
        {
            await Task.WhenAll(
            [
                _tokensService.DeleteAccessToken(),
                _tokensService.DeleteRefreshToken()
            ]);
        }
        return response;
    }
    
    #endregion
    
    
    
    #region PRIVATE METHODS

    private async Task UpdateTokens(AuthenticationResponse tokens) => await Task.WhenAll(
    [
        _tokensService.SetAccessToken(tokens.AccessToken), 
        _tokensService.SetRefreshToken(tokens.RefreshToken)
    ]);

    private static bool ValidateToken(string token)
    {
        IEnumerable<Claim> claims = GetClaimsFromToken(token);
        Claim? claim = claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp);
        if (claim is null || !long.TryParse(claim.Value, out long expiration))
        {
            return false;
        }
        DateTime expirationDate = DateTime.UnixEpoch.AddSeconds(expiration);
        return expirationDate > DateTime.UtcNow;
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
            return [];
        }

        return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
    }
    
    #endregion
}