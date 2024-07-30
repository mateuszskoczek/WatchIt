using WatchIt.Common.Model.Accounts;
using WatchIt.Common.Services.HttpClient;
using WatchIt.Website.Services.Utility.Configuration;
using WatchIt.Website.Services.Utility.Configuration.Model;
using WatchIt.Website.Services.Utility.Tokens;
using WatchIt.Website.Services.WebAPI.Common;

namespace WatchIt.Website.Services.WebAPI.Accounts;

public class AccountsWebAPIService(IHttpClientService httpClientService, IConfigurationService configurationService, ITokensService tokensService) : BaseWebAPIService(configurationService), IAccountsWebAPIService
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
    
    #endregion



    #region PRIVATE METHODS

    protected override string GetServiceBase() => EndpointsConfiguration.Accounts.Base;

    #endregion
}