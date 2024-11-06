using WatchIt.Common.Model.Accounts;
using WatchIt.Common.Model.Movies;
using WatchIt.Common.Model.Persons;
using WatchIt.Common.Model.Photos;
using WatchIt.Common.Model.Series;
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
        string url = GetUrl(EndpointsConfiguration.Accounts.GetAccountProfilePicture, id);
        HttpRequest request = new HttpRequest(HttpMethodType.Get, url);
        
        HttpResponse response = await httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
            .RegisterActionFor400BadRequest(badRequestAction)
            .RegisterActionFor404NotFound(notFoundAction)
            .ExecuteAction();
    }

    public async Task PutAccountProfilePicture(AccountProfilePictureRequest data, Action<AccountProfilePictureResponse>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Accounts.PutAccountProfilePicture);
        
        HttpRequest request = new HttpRequest(HttpMethodType.Put, url)
        {
            Body = data
        };
        
        HttpResponse response = await httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor400BadRequest(badRequestAction)
                .RegisterActionFor401Unauthorized(unauthorizedAction)
                .ExecuteAction();
    }
    
    public async Task DeleteAccountProfilePicture(Action? successAction = null, Action? unauthorizedAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Accounts.DeleteAccountProfilePicture);
        
        HttpRequest request = new HttpRequest(HttpMethodType.Delete, url);
        
        HttpResponse response = await httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor401Unauthorized(unauthorizedAction)
                .ExecuteAction();
    }

    public async Task GetAccountProfileBackground(long id, Action<PhotoResponse>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? notFoundAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Accounts.GetAccountProfileBackground, id);
        HttpRequest request = new HttpRequest(HttpMethodType.Get, url);
        
        HttpResponse response = await httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor400BadRequest(badRequestAction)
                .RegisterActionFor404NotFound(notFoundAction)
                .ExecuteAction();
    }
    
    public async Task PutAccountProfileBackground(AccountProfileBackgroundRequest data, Action<PhotoResponse>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Accounts.PutAccountProfileBackground);
        
        HttpRequest request = new HttpRequest(HttpMethodType.Put, url)
        {
            Body = data
        };
        
        HttpResponse response = await httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor400BadRequest(badRequestAction)
                .RegisterActionFor401Unauthorized(unauthorizedAction)
                .ExecuteAction();
    }
    
    public async Task DeleteAccountProfileBackground(Action? successAction = null, Action? unauthorizedAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Accounts.DeleteAccountProfileBackground);
        
        HttpRequest request = new HttpRequest(HttpMethodType.Delete, url);
        
        HttpResponse response = await httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor401Unauthorized(unauthorizedAction)
                .ExecuteAction();
    }
    
    public async Task GetAccounts(AccountQueryParameters query, Action<IEnumerable<AccountResponse>>? successAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Accounts.GetAccounts);
        HttpRequest request = new HttpRequest(HttpMethodType.Get, url)
        {
            Query = query
        };
        
        HttpResponse response = await httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .ExecuteAction();
    }
    
    public async Task GetAccount(long id, Action<AccountResponse>? successAction = null, Action? notFoundAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Accounts.GetAccount, id);
        HttpRequest request = new HttpRequest(HttpMethodType.Get, url);
        
        HttpResponse response = await httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor404NotFound(notFoundAction)
                .ExecuteAction();
    }
    
    public async Task PutAccountProfileInfo(AccountProfileInfoRequest data, Action? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Accounts.PutAccountProfileInfo);
        HttpRequest request = new HttpRequest(HttpMethodType.Put, url)
        {
            Body = data,
        };
        
        HttpResponse response = await httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor400BadRequest(badRequestAction)
                .RegisterActionFor401Unauthorized(unauthorizedAction)
                .ExecuteAction();
    }
    
    public async Task PatchAccountUsername(AccountUsernameRequest data, Action? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Accounts.PatchAccountUsername);
        HttpRequest request = new HttpRequest(HttpMethodType.Patch, url)
        {
            Body = data,
        };
        
        HttpResponse response = await httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor400BadRequest(badRequestAction)
                .RegisterActionFor401Unauthorized(unauthorizedAction)
                .ExecuteAction();
    }
    
    public async Task PatchAccountEmail(AccountEmailRequest data, Action? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Accounts.PatchAccountEmail);
        HttpRequest request = new HttpRequest(HttpMethodType.Patch, url)
        {
            Body = data,
        };
        
        HttpResponse response = await httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor400BadRequest(badRequestAction)
                .RegisterActionFor401Unauthorized(unauthorizedAction)
                .ExecuteAction();
    }
    
    public async Task PatchAccountPassword(AccountPasswordRequest data, Action? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Accounts.PatchAccountPassword);
        HttpRequest request = new HttpRequest(HttpMethodType.Patch, url)
        {
            Body = data,
        };
        
        HttpResponse response = await httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor400BadRequest(badRequestAction)
                .RegisterActionFor401Unauthorized(unauthorizedAction)
                .ExecuteAction();
    }
    
    public async Task GetAccountRatedMovies(long id, MovieRatedQueryParameters query, Action<IEnumerable<MovieRatedResponse>>? successAction = null, Action? notFoundAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Accounts.GetAccountRatedMovies, id);
        HttpRequest request = new HttpRequest(HttpMethodType.Get, url)
        {
            Query = query
        };
        
        HttpResponse response = await httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor404NotFound(notFoundAction)
                .ExecuteAction();
    }
    
    public async Task GetAccountRatedSeries(long id, SeriesRatedQueryParameters query, Action<IEnumerable<SeriesRatedResponse>>? successAction = null, Action? notFoundAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Accounts.GetAccountRatedSeries, id);
        HttpRequest request = new HttpRequest(HttpMethodType.Get, url)
        {
            Query = query
        };
        
        HttpResponse response = await httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor404NotFound(notFoundAction)
                .ExecuteAction();
    }
    
    public async Task GetAccountRatedPersons(long id, PersonRatedQueryParameters query, Action<IEnumerable<PersonRatedResponse>>? successAction = null, Action? notFoundAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Accounts.GetAccountRatedPersons, id);
        HttpRequest request = new HttpRequest(HttpMethodType.Get, url)
        {
            Query = query
        };
        
        HttpResponse response = await httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor404NotFound(notFoundAction)
                .ExecuteAction();
    }
    
    #endregion



    #region PRIVATE METHODS

    protected override string GetServiceBase() => EndpointsConfiguration.Accounts.Base;

    #endregion
}