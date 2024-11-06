using System.Text;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Primitives;
using WatchIt.Common.Model.Movies;
using WatchIt.Common.Services.HttpClient;
using WatchIt.Website.Services.Configuration;

namespace WatchIt.Website.Services.Client.Movies;

public class MoviesClientService : BaseClientService, IMoviesClientService
{
    #region SERVICES

    private IHttpClientService _httpClientService;
    private IConfigurationService _configurationService;

    #endregion

    
    
    #region CONSTRUCTORS

    public MoviesClientService(IHttpClientService httpClientService, IConfigurationService configurationService) : base(configurationService)
    {
        _httpClientService = httpClientService;
        _configurationService = configurationService;
    }

    #endregion


    
    #region PUBLIC METHODS
    
    #region Main

    public async Task GetAllMovies(MovieQueryParameters? query = null, Action<IEnumerable<MovieResponse>>? successAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Movies.GetAllMovies);
        
        HttpRequest request = new HttpRequest(HttpMethodType.Get, url);
        request.Query = query;
        
        HttpResponse response = await _httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .ExecuteAction();
    }
    
    public async Task GetMovie(long id, Action<MovieResponse>? successAction = null, Action? notFoundAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Movies.GetMovie, id);
        
        HttpRequest request = new HttpRequest(HttpMethodType.Get, url);
        
        HttpResponse response = await _httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor404NotFound(notFoundAction)
                .ExecuteAction();
    }
    
    public async Task PostMovie(MovieRequest data, Action<MovieResponse>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Movies.PostMovie);
        
        HttpRequest request = new HttpRequest(HttpMethodType.Post, url);
        request.Body = data;
        
        HttpResponse response = await _httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor400BadRequest(badRequestAction)
                .RegisterActionFor401Unauthorized(unauthorizedAction)
                .RegisterActionFor403Forbidden(forbiddenAction)
                .ExecuteAction();
    }
    
    public async Task PutMovie(long id, MovieRequest data, Action? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Movies.PutMovie, id);
        
        HttpRequest request = new HttpRequest(HttpMethodType.Put, url);
        request.Body = data;
        
        HttpResponse response = await _httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor400BadRequest(badRequestAction)
                .RegisterActionFor401Unauthorized(unauthorizedAction)
                .RegisterActionFor403Forbidden(forbiddenAction)
                .ExecuteAction();
    }
    
    public async Task DeleteMovie(long id, Action? successAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Movies.DeleteMovie, id);
        
        HttpRequest request = new HttpRequest(HttpMethodType.Delete, url);
        
        HttpResponse response = await _httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor401Unauthorized(unauthorizedAction)
                .RegisterActionFor403Forbidden(forbiddenAction)
                .ExecuteAction();
    }
    
    #endregion

    #region View count

    public async Task GetMoviesViewRank(int? first = null, int? days = null, Action<IEnumerable<MovieResponse>>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Movies.GetMoviesViewRank);
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

    protected override string GetServiceBase() => EndpointsConfiguration.Movies.Base;

    #endregion
}