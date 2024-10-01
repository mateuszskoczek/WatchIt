using WatchIt.Common.Model.Genres;
using WatchIt.Common.Model.Media;
using WatchIt.Common.Model.Photos;
using WatchIt.Common.Model.Rating;
using WatchIt.Common.Services.HttpClient;
using WatchIt.Website.Services.Utility.Configuration;
using WatchIt.Website.Services.Utility.Configuration.Model;
using WatchIt.Website.Services.WebAPI.Common;

namespace WatchIt.Website.Services.WebAPI.Media;

public class MediaWebAPIService : BaseWebAPIService, IMediaWebAPIService
{
    #region FIELDS
    
    private readonly IHttpClientService _httpClientService;
    
    #endregion



    #region CONSTRUCTORS

    public MediaWebAPIService(IHttpClientService httpClientService, IConfigurationService configurationService) : base(configurationService)
    {
        _httpClientService = httpClientService;
    }

    #endregion
    
    
    
    #region PUBLIC METHODS
    
    #region Main
    
    public async Task GetMedia(long mediaId, Action<MediaResponse>? successAction = null, Action? notFoundAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Media.GetMedia, mediaId);
        
        HttpRequest request = new HttpRequest(HttpMethodType.Get, url);
        
        HttpResponse response = await _httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor404NotFound(notFoundAction)
                .ExecuteAction();
    }
    
    #endregion

    #region Genres

    public async Task GetMediaGenres(long mediaId, Action<IEnumerable<GenreResponse>>? successAction = null, Action? notFoundAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Media.GetMediaGenres, mediaId);
        
        HttpRequest request = new HttpRequest(HttpMethodType.Get, url);
        
        HttpResponse response = await _httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor404NotFound(notFoundAction)
                .ExecuteAction();
    }
    
    public async Task PostMediaGenre(long mediaId, long genreId, Action? successAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null, Action? notFoundAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Media.PostMediaGenre, mediaId, genreId);
        
        HttpRequest request = new HttpRequest(HttpMethodType.Post, url);
        
        HttpResponse response = await _httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor401Unauthorized(unauthorizedAction)
                .RegisterActionFor403Forbidden(forbiddenAction)
                .RegisterActionFor404NotFound(notFoundAction)
                .ExecuteAction();
    }
    
    public async Task DeleteMediaGenre(long mediaId, long genreId, Action? successAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null, Action? notFoundAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Media.DeleteMediaGenre, mediaId, genreId);
        
        HttpRequest request = new HttpRequest(HttpMethodType.Delete, url);
        
        HttpResponse response = await _httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor401Unauthorized(unauthorizedAction)
                .RegisterActionFor403Forbidden(forbiddenAction)
                .RegisterActionFor404NotFound(notFoundAction)
                .ExecuteAction();
    }

    #endregion

    #region Rating

    public async Task GetMediaRating(long mediaId, Action<RatingResponse>? successAction = null, Action? notFoundAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Media.GetMediaRating, mediaId);
        
        HttpRequest request = new HttpRequest(HttpMethodType.Get, url);
        
        HttpResponse response = await _httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor404NotFound(notFoundAction)
                .ExecuteAction();
    }

    public async Task GetMediaRatingByUser(long mediaId, long userId, Action<short>? successAction = null, Action? notFoundAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Media.GetMediaRatingByUser, mediaId, userId);
        
        HttpRequest request = new HttpRequest(HttpMethodType.Get, url);
        
        HttpResponse response = await _httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor404NotFound(notFoundAction)
                .ExecuteAction();
    }

    public async Task PutMediaRating(long mediaId, RatingRequest body, Action? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null, Action? notFoundAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Media.PutMediaRating, mediaId);
        
        HttpRequest request = new HttpRequest(HttpMethodType.Put, url)
        {
            Body = body
        };
        
        HttpResponse response = await _httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor400BadRequest(badRequestAction)
                .RegisterActionFor401Unauthorized(unauthorizedAction)
                .RegisterActionFor404NotFound(notFoundAction)
                .ExecuteAction();
    }
    
    public async Task DeleteMediaRating(long mediaId, Action? successAction = null, Action? unauthorizedAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Media.DeleteMediaRating, mediaId);
        
        HttpRequest request = new HttpRequest(HttpMethodType.Delete, url);
        
        HttpResponse response = await _httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor401Unauthorized(unauthorizedAction)
                .ExecuteAction();
    }

    #endregion

    #region View count

    public async Task PostMediaView(long mediaId, Action? successAction = null, Action? notFoundAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Media.PostMediaView, mediaId);
        
        HttpRequest request = new HttpRequest(HttpMethodType.Post, url);
        
        HttpResponse response = await _httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor404NotFound(notFoundAction)
                .ExecuteAction();
    }

    #endregion
    
    #region Poster
    
    public async Task GetMediaPoster(long mediaId, Action<MediaPosterResponse>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? notFoundAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Media.GetMediaPoster, mediaId);
        
        HttpRequest request = new HttpRequest(HttpMethodType.Get, url);
        
        HttpResponse response = await _httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor400BadRequest(badRequestAction)
                .RegisterActionFor404NotFound(notFoundAction)
                .ExecuteAction();
    }

    public async Task PutMediaPoster(long mediaId, MediaPosterRequest data, Action<MediaPosterResponse>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Media.PutMediaPoster, mediaId);
        
        HttpRequest request = new HttpRequest(HttpMethodType.Put, url)
        {
            Body = data
        };
        
        HttpResponse response = await _httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor400BadRequest(badRequestAction)
                .RegisterActionFor401Unauthorized(unauthorizedAction)
                .RegisterActionFor403Forbidden(forbiddenAction)
                .ExecuteAction();
    }
    
    public async Task DeleteMediaPoster(long mediaId, Action? successAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Media.DeleteMediaPoster, mediaId);
        
        HttpRequest request = new HttpRequest(HttpMethodType.Delete, url);
        
        HttpResponse response = await _httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor401Unauthorized(unauthorizedAction)
                .RegisterActionFor403Forbidden(forbiddenAction)
                .ExecuteAction();
    }

    #endregion

    #region Photos

    public async Task GetMediaPhotos(long mediaId, PhotoQueryParameters? query = null, Action<IEnumerable<PhotoResponse>>? successAction = null, Action? notFoundAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Media.GetMediaPhotos, mediaId);
        
        HttpRequest request = new HttpRequest(HttpMethodType.Get, url);
        
        HttpResponse response = await _httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor404NotFound(notFoundAction)
                .ExecuteAction();
    }

    public async Task GetMediaPhotoRandomBackground(long mediaId, Action<PhotoResponse>? successAction = null, Action? notFoundAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Media.GetMediaPhotoRandomBackground, mediaId);
        
        HttpRequest request = new HttpRequest(HttpMethodType.Get, url);
        
        HttpResponse response = await _httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor404NotFound(notFoundAction)
                .ExecuteAction();
    }

    public async Task PostMediaPhoto(long mediaId, MediaPhotoRequest data, Action? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null, Action? notFoundAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Media.PostMediaPhoto, mediaId);
        
        HttpRequest request = new HttpRequest(HttpMethodType.Post, url)
        {
            Body = data
        };
        
        HttpResponse response = await _httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor400BadRequest(badRequestAction)
                .RegisterActionFor401Unauthorized(unauthorizedAction)
                .RegisterActionFor403Forbidden(forbiddenAction)
                .RegisterActionFor404NotFound(notFoundAction)
                .ExecuteAction();
    }

    #endregion
    
    #endregion

    
    
    #region PRIVATE METHODS

    protected override string GetServiceBase() => EndpointsConfiguration.Media.Base;

    #endregion
}