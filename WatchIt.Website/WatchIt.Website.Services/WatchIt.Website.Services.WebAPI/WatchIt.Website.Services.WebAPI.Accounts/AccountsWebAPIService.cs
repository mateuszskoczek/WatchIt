using WatchIt.Common.Model.Accounts;
using WatchIt.Common.Services.HttpClient;
using WatchIt.Website.Services.Utility.Configuration;
using WatchIt.Website.Services.Utility.Configuration.Model;
using WatchIt.Website.Services.WebAPI.Common;

namespace WatchIt.Website.Services.WebAPI.Accounts;

public class AccountsWebAPIService(IHttpClientService httpClientService, IConfigurationService configurationService) : BaseWebAPIService(configurationService), IAccountsWebAPIService
{
    #region PUBLIC METHODS
    
    public async Task Register(RegisterRequest data, Action<RegisterResponse> createdAction, Action<IDictionary<string, string[]>> badRequestAction)
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
    
    public async Task Authenticate(AuthenticateRequest data, Action<AuthenticateResponse> successAction, Action<IDictionary<string, string[]>> badRequestAction, Action unauthorizedAction)
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
    
    public async Task AuthenticateRefresh(Action<AuthenticateResponse> successAction, Action unauthorizedAction, Action forbiddenAction)
    {
        string url = GetUrl(EndpointsConfiguration.Accounts.AuthenticateRefresh);
        HttpRequest request = new HttpRequest(HttpMethodType.Post, url);
        
        HttpResponse response = await httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor401Unauthorized(unauthorizedAction)
                .RegisterActionFor403Forbidden(forbiddenAction)
                .ExecuteAction();
    }
    
    #endregion



    #region PRIVATE METHODS

    protected override string GetServiceBase() => EndpointsConfiguration.Accounts.Base;

    #endregion
}