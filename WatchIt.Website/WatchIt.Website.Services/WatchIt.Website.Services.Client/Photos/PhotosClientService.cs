using WatchIt.Common.Model.Photos;
using WatchIt.Common.Services.HttpClient;
using WatchIt.Website.Services.Configuration;

namespace WatchIt.Website.Services.Client.Photos;

public class PhotosClientService : BaseClientService, IPhotosClientService
{
    #region FIELDS
    
    private readonly IHttpClientService _httpClientService;
    
    #endregion



    #region CONSTRUCTORS

    public PhotosClientService(IHttpClientService httpClientService, IConfigurationService configurationService) : base(configurationService)
    {
        _httpClientService = httpClientService;
    }

    #endregion
    
    
    
    #region PUBLIC METHODS
    
    #region Main
    
    public async Task GetPhotoRandomBackground(Action<PhotoResponse>? successAction = null, Action? notFoundAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Photos.GetPhotoRandomBackground);
        
        HttpRequest request = new HttpRequest(HttpMethodType.Get, url);
        
        HttpResponse response = await _httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor404NotFound(notFoundAction)
                .ExecuteAction();
    }
    
    public async Task DeletePhoto(Guid id, Action? successAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null, Action? notFoundAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Photos.DeletePhoto, id);
        
        HttpRequest request = new HttpRequest(HttpMethodType.Delete, url);
        
        HttpResponse response = await _httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor401Unauthorized(unauthorizedAction)
                .RegisterActionFor403Forbidden(forbiddenAction)
                .RegisterActionFor404NotFound(notFoundAction)
                .ExecuteAction();
    }
    
    #endregion

    #region Background data

    public async Task PutPhotoBackgroundData(Guid id, PhotoBackgroundDataRequest data, Action? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null, Action? notFoundAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Photos.PutPhotoBackgroundData, id);
        
        HttpRequest request = new HttpRequest(HttpMethodType.Put, url)
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
    
    public async Task DeletePhotoBackgroundData(Guid id, Action? successAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null, Action? notFoundAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Photos.DeletePhotoBackgroundData, id);
        
        HttpRequest request = new HttpRequest(HttpMethodType.Delete, url);
        
        HttpResponse response = await _httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor401Unauthorized(unauthorizedAction)
                .RegisterActionFor403Forbidden(forbiddenAction)
                .RegisterActionFor404NotFound(notFoundAction)
                .ExecuteAction();
    }

    #endregion
    
    #endregion

    
    
    #region PRIVATE METHODS

    protected override string GetServiceBase() => EndpointsConfiguration.Photos.Base;

    #endregion
}