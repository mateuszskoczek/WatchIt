using System.Text;
using WatchIt.Common.Model.Series;
using WatchIt.Common.Services.HttpClient;
using WatchIt.Website.Services.Utility.Configuration;
using WatchIt.Website.Services.WebAPI.Common;

namespace WatchIt.Website.Services.WebAPI.Series;

public class SeriesWebAPIService : BaseWebAPIService, ISeriesWebAPIService
{
    #region SERVICES

    private IHttpClientService _httpClientService;
    private IConfigurationService _configurationService;

    #endregion

    
    
    #region CONSTRUCTORS

    public SeriesWebAPIService(IHttpClientService httpClientService, IConfigurationService configurationService) : base(configurationService)
    {
        _httpClientService = httpClientService;
        _configurationService = configurationService;
    }

    #endregion


    
    #region PUBLIC METHODS
    
    #region Main

    public async Task GetAllSeries(SeriesQueryParameters? query = null, Action<IEnumerable<SeriesResponse>>? successAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Series.GetAllSeries);
        
        HttpRequest request = new HttpRequest(HttpMethodType.Get, url);
        request.Query = query;
        
        HttpResponse response = await _httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .ExecuteAction();
    }
    
    public async Task GetSeries(long id, Action<SeriesResponse>? successAction = null, Action? notFoundAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Series.GetSeries, id);
        
        HttpRequest request = new HttpRequest(HttpMethodType.Get, url);
        
        HttpResponse response = await _httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor404NotFound(notFoundAction)
                .ExecuteAction();
    }
    
    public async Task PostSeries(SeriesRequest data, Action<SeriesResponse>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Series.PostSeries);
        
        HttpRequest request = new HttpRequest(HttpMethodType.Post, url);
        request.Body = data;
        
        HttpResponse response = await _httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor400BadRequest(badRequestAction)
                .RegisterActionFor401Unauthorized(unauthorizedAction)
                .RegisterActionFor403Forbidden(forbiddenAction)
                .ExecuteAction();
    }
    
    public async Task PutSeries(long id, SeriesRequest data, Action? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Series.PutSeries, id);
        
        HttpRequest request = new HttpRequest(HttpMethodType.Put, url);
        request.Body = data;
        
        HttpResponse response = await _httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor400BadRequest(badRequestAction)
                .RegisterActionFor401Unauthorized(unauthorizedAction)
                .RegisterActionFor403Forbidden(forbiddenAction)
                .ExecuteAction();
    }
    
    public async Task DeleteSeries(long id, Action? successAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Series.DeleteSeries, id);
        
        HttpRequest request = new HttpRequest(HttpMethodType.Delete, url);
        
        HttpResponse response = await _httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor401Unauthorized(unauthorizedAction)
                .RegisterActionFor403Forbidden(forbiddenAction)
                .ExecuteAction();
    }
    
    #endregion

    #region View count

    public async Task GetSeriesViewRank(int? first = null, int? days = null, Action<IEnumerable<SeriesResponse>>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Series.GetSeriesViewRank);
        if (first.HasValue || days.HasValue)
        {
            StringBuilder urlBuilder = new StringBuilder(url);
            urlBuilder.Append('?');
            bool firstParameter = true;
            if (first.HasValue)
            {
                urlBuilder.Append($"first={first.Value}");
                firstParameter = false;
            }
            if (days.HasValue)
            {
                if (!firstParameter)
                {
                    urlBuilder.Append('&');
                }
                urlBuilder.Append($"days={days.Value}");
            }
            url = urlBuilder.ToString();
        }
        
        HttpRequest request = new HttpRequest(HttpMethodType.Get, url);
        
        HttpResponse response = await _httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor400BadRequest(badRequestAction)
                .ExecuteAction();
    }

    #endregion

    #endregion
    
    
    
    #region PRIVATE METHODS

    protected override string GetServiceBase() => EndpointsConfiguration.Series.Base;

    #endregion
}