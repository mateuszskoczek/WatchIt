using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.Authorization;
using WatchIt.Website.Services.Utility.Tokens;
using WatchIt.Website.Services.WebAPI.Accounts;

namespace WatchIt.Website.Services.Utility.Authentication;

public class AuthenticationService : IAuthenticationService
{
    #region SERVICES

    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly HttpClient _httpClient;
    private readonly ITokensService _tokensService;
    private readonly IAccountsWebAPIService _accountsWebAPIService;
    
    #endregion
    
    
    
    #region CONSTRUCTORS

    public AuthenticationService(AuthenticationStateProvider authenticationStateProvider, HttpClient httpClient, ITokensService tokensService, IAccountsWebAPIService accountsWebAPIService)
    {
        _authenticationStateProvider = authenticationStateProvider;
        _httpClient = httpClient;
        _tokensService = tokensService;
        _accountsWebAPIService = accountsWebAPIService;
    }
    
    #endregion
    
    
    
    #region PUBLIC METHODS

    public async Task<User?> GetUserAsync()
    {
        AuthenticationState state = await _authenticationStateProvider.GetAuthenticationStateAsync();
        
        if (!GetAuthenticationStatusAsync(state))
        {
            return null;
        }

        return new User
        {
            Id = int.Parse(state.User.FindFirst(x => x.Type == JwtRegisteredClaimNames.Sub)!.Value),
            Username = state.User.FindFirst(x => x.Type == JwtRegisteredClaimNames.UniqueName)!.Value,
            Email = state.User.FindFirst(x => x.Type == JwtRegisteredClaimNames.Email)!.Value,
            IsAdmin = bool.Parse(state.User.FindFirst(x => x.Type == "admin")!.Value),
        };
    }

    public async Task<bool> GetAuthenticationStatusAsync()
    {
        AuthenticationState state = await _authenticationStateProvider.GetAuthenticationStateAsync();
        return GetAuthenticationStatusAsync(state);
    }

    public async Task LogoutAsync()
    {
        string? refreshToken = await _tokensService.GetRefreshToken();
        if (refreshToken is not null)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("refresh", refreshToken.Replace("\"", ""));
            await _accountsWebAPIService.Logout();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
    
    #endregion



    #region PRIVATE METHODS

    private bool GetAuthenticationStatusAsync(AuthenticationState state)
    {
        return state.User.HasClaim(x => x.Type == JwtRegisteredClaimNames.Iss && x.Value == "WatchIt");
    }

    #endregion
}