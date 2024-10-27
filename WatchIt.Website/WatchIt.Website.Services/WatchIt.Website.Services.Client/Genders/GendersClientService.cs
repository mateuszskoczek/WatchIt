using WatchIt.Common.Model.Genders;
using WatchIt.Common.Services.HttpClient;
using WatchIt.Website.Services.Configuration;

namespace WatchIt.Website.Services.Client.Genders;

public class GendersClientService : BaseClientService, IGendersClientService
{
    #region SERVICES

    private IHttpClientService _httpClientService;
    private IConfigurationService _configurationService;

    #endregion

    
    
    #region CONSTRUCTORS

    public GendersClientService(IHttpClientService httpClientService, IConfigurationService configurationService) : base(configurationService)
    {
        _httpClientService = httpClientService;
        _configurationService = configurationService;
    }

    #endregion


    
    #region PUBLIC METHODS
    
    #region Main

    public async Task GetAllGenders(GenderQueryParameters? query = null, Action<IEnumerable<GenderResponse>>? successAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Genders.GetAllGenders);
        
        HttpRequest request = new HttpRequest(HttpMethodType.Get, url);
        request.Query = query;
        
        HttpResponse response = await _httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .ExecuteAction();
    }
    
    public async Task GetGender(long id, Action<GenderResponse>? successAction = null, Action? notFoundAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Genders.GetGender, id);
        
        HttpRequest request = new HttpRequest(HttpMethodType.Get, url);
        
        HttpResponse response = await _httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor404NotFound(notFoundAction)
                .ExecuteAction();
    }
    
    public async Task PostGender(GenderRequest data, Action<GenderResponse>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Genders.PostGender);
        
        HttpRequest request = new HttpRequest(HttpMethodType.Post, url);
        request.Body = data;
        
        HttpResponse response = await _httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor400BadRequest(badRequestAction)
                .RegisterActionFor401Unauthorized(unauthorizedAction)
                .RegisterActionFor403Forbidden(forbiddenAction)
                .ExecuteAction();
    }
    
    public async Task DeleteGender(long id, Action? successAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Genders.DeleteGender, id);
        
        HttpRequest request = new HttpRequest(HttpMethodType.Delete, url);
        
        HttpResponse response = await _httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor401Unauthorized(unauthorizedAction)
                .RegisterActionFor403Forbidden(forbiddenAction)
                .ExecuteAction();
    }
    
    #endregion

    #endregion
    
    
    
    #region PRIVATE METHODS

    protected override string GetServiceBase() => EndpointsConfiguration.Genders.Base;

    #endregion
}