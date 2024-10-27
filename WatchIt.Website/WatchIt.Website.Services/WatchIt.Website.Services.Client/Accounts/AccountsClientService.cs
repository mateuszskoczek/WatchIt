﻿using WatchIt.Common.Model.Accounts;
using WatchIt.Common.Services.HttpClient;
using WatchIt.Website.Services.Configuration;
using WatchIt.Website.Services.Tokens;

namespace WatchIt.Website.Services.Client.Accounts;

public class AccountsClientService(IHttpClientService httpClientService, IConfigurationService configurationService, ITokensService tokensService) : BaseClientService(configurationService), IAccountsClientService
{
    #region PUBLIC METHODS
    
    public async Task Register(RegisterRequest data, Action<RegisterResponse>? createdAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Accounts.Register);
        HttpRequest request = new HttpRequest(HttpMethodType.Post, url)
        {
            Body = data,
        };
        
        HttpResponse response = await httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(createdAction)
                .RegisterActionFor400BadRequest(badRequestAction)
                .ExecuteAction();
    }
    
    public async Task Authenticate(AuthenticateRequest data, Action<AuthenticateResponse>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Accounts.Authenticate);
        HttpRequest request = new HttpRequest(HttpMethodType.Post, url)
        {
            Body = data,
        };
        
        HttpResponse response = await httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor400BadRequest(badRequestAction)
                .RegisterActionFor401Unauthorized(unauthorizedAction)
                .ExecuteAction();
    }
    
    public async Task AuthenticateRefresh(Action<AuthenticateResponse>? successAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Accounts.AuthenticateRefresh);
        string? token = await tokensService.GetRefreshToken();
        
        HttpRequest request = new HttpRequest(HttpMethodType.Post, url);
        request.Headers.Add("Authorization", $"Bearer {token}");
        
        HttpResponse response = await httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor401Unauthorized(unauthorizedAction)
                .RegisterActionFor403Forbidden(forbiddenAction)
                .ExecuteAction();
    }

    public async Task Logout(Action? successAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Accounts.Logout);
        string? token = await tokensService.GetRefreshToken();
        
        HttpRequest request = new HttpRequest(HttpMethodType.Delete, url);
        request.Headers.Add("Authorization", $"Bearer {token}");
        
        HttpResponse response = await httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .ExecuteAction();
    }

    public async Task GetAccountProfilePicture(long id, Action<AccountProfilePictureResponse>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? notFoundAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Accounts.GetProfilePicture, id);
        HttpRequest request = new HttpRequest(HttpMethodType.Get, url);
        
        HttpResponse response = await httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
            .RegisterActionFor400BadRequest(badRequestAction)
            .RegisterActionFor404NotFound(notFoundAction)
            .ExecuteAction();
    }
    
    public async Task GetAccountInfoById(long id, Action<AccountResponse>? successAction = null, Action? notFoundAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Accounts.GetAccountInfoById, id);
        HttpRequest request = new HttpRequest(HttpMethodType.Get, url);
        
        HttpResponse response = await httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor404NotFound(notFoundAction)
                .ExecuteAction();
    }
    
    public async Task GetAccountInfo(Action<AccountResponse>? successAction = null, Action? unauthorizedAction = null, Action? notFoundAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Accounts.GetAccountInfo);
        HttpRequest request = new HttpRequest(HttpMethodType.Get, url);
        
        HttpResponse response = await httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor401Unauthorized(unauthorizedAction)
                .RegisterActionFor404NotFound(notFoundAction)
                .ExecuteAction();
    }
    
    public async Task PutAccountInfo(AccountRequest data, Action<AccountResponse>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null, Action? notFoundAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Accounts.PutAccountInfo);
        HttpRequest request = new HttpRequest(HttpMethodType.Put, url)
        {
            Body = data,
        };
        
        HttpResponse response = await httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor400BadRequest(badRequestAction)
                .RegisterActionFor401Unauthorized(unauthorizedAction)
                .RegisterActionFor404NotFound(notFoundAction)
                .ExecuteAction();
    }
    
    #endregion



    #region PRIVATE METHODS

    protected override string GetServiceBase() => EndpointsConfiguration.Accounts.Base;

    #endregion
}