using WatchIt.Common.Model.Roles;
using WatchIt.Common.Services.HttpClient;
using WatchIt.Website.Services.Utility.Configuration;
using WatchIt.Website.Services.WebAPI.Common;

namespace WatchIt.Website.Services.WebAPI.Roles;

public class RolesWebAPIService : BaseWebAPIService, IRolesWebAPIService
{
    #region SERVICES

    private IHttpClientService _httpClientService;

    #endregion

    
    
    #region CONSTRUCTORS

    public RolesWebAPIService(IHttpClientService httpClientService, IConfigurationService configurationService) : base(configurationService)
    {
        _httpClientService = httpClientService;
    }

    #endregion


    
    #region PUBLIC METHODS
    
    #region Actor
    
    public async Task GetActorRole(Guid id, Action<ActorRoleResponse>? successAction = null, Action? notFoundAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Roles.GetActorRole, id);
        
        HttpRequest request = new HttpRequest(HttpMethodType.Get, url);
        
        HttpResponse response = await _httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor404NotFound(notFoundAction)
                .ExecuteAction();
    }
    
    public async Task PutActorRole(Guid id, ActorRoleUniversalRequest data, Action? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null, Action? notFoundAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Roles.PutActorRole, id);
        
        HttpRequest request = new HttpRequest(HttpMethodType.Put, url);
        request.Body = data;
        
        HttpResponse response = await _httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor400BadRequest(badRequestAction)
                .RegisterActionFor401Unauthorized(unauthorizedAction)
                .RegisterActionFor403Forbidden(forbiddenAction)
                .RegisterActionFor404NotFound(notFoundAction)
                .ExecuteAction();
    }
    
    public async Task DeleteActorRole(Guid id, Action? successAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Roles.DeleteActorRole, id);
        
        HttpRequest request = new HttpRequest(HttpMethodType.Delete, url);
        
        HttpResponse response = await _httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor401Unauthorized(unauthorizedAction)
                .RegisterActionFor403Forbidden(forbiddenAction)
                .ExecuteAction();
    }

    public async Task GetAllActorRoleTypes(RoleTypeQueryParameters? query = null, Action<IEnumerable<RoleTypeResponse>>? successAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Roles.GetAllActorRoleTypes);
        
        HttpRequest request = new HttpRequest(HttpMethodType.Get, url);
        request.Query = query;
        
        HttpResponse response = await _httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .ExecuteAction();
    }
    
    public async Task GetActorRoleType(long typeId, Action<RoleTypeResponse>? successAction = null, Action? notFoundAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Roles.GetActorRoleType, typeId);
        
        HttpRequest request = new HttpRequest(HttpMethodType.Get, url);
        
        HttpResponse response = await _httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor404NotFound(notFoundAction)
                .ExecuteAction();
    }
    
    public async Task PostActorRoleType(RoleTypeRequest data, Action<RoleTypeResponse>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Roles.PostActorRoleType);
        
        HttpRequest request = new HttpRequest(HttpMethodType.Post, url);
        request.Body = data;
        
        HttpResponse response = await _httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor400BadRequest(badRequestAction)
                .RegisterActionFor401Unauthorized(unauthorizedAction)
                .RegisterActionFor403Forbidden(forbiddenAction)
                .ExecuteAction();
    }
    
    public async Task DeleteActorRoleType(long typeId, Action? successAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Roles.DeleteActorRoleType, typeId);
        
        HttpRequest request = new HttpRequest(HttpMethodType.Delete, url);
        
        HttpResponse response = await _httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor401Unauthorized(unauthorizedAction)
                .RegisterActionFor403Forbidden(forbiddenAction)
                .ExecuteAction();
    }
    
    #endregion
    
    #region Creator

    public async Task GetCreatorRole(Guid id, Action<CreatorRoleResponse>? successAction = null, Action? notFoundAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Roles.GetCreatorRole, id);
        
        HttpRequest request = new HttpRequest(HttpMethodType.Get, url);
        
        HttpResponse response = await _httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor404NotFound(notFoundAction)
                .ExecuteAction();
    }
    
    public async Task PutCreatorRole(Guid id, CreatorRoleUniversalRequest data, Action? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null, Action? notFoundAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Roles.PutCreatorRole, id);
        
        HttpRequest request = new HttpRequest(HttpMethodType.Put, url);
        request.Body = data;
        
        HttpResponse response = await _httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor400BadRequest(badRequestAction)
                .RegisterActionFor401Unauthorized(unauthorizedAction)
                .RegisterActionFor403Forbidden(forbiddenAction)
                .RegisterActionFor404NotFound(notFoundAction)
                .ExecuteAction();
    }
    
    public async Task DeleteCreatorRole(Guid id, Action? successAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Roles.DeleteCreatorRole, id);
        
        HttpRequest request = new HttpRequest(HttpMethodType.Delete, url);
        
        HttpResponse response = await _httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor401Unauthorized(unauthorizedAction)
                .RegisterActionFor403Forbidden(forbiddenAction)
                .ExecuteAction();
    }
    
    public async Task GetAllCreatorRoleTypes(RoleTypeQueryParameters? query = null, Action<IEnumerable<RoleTypeResponse>>? successAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Roles.GetAllCreatorRoleTypes);
        
        HttpRequest request = new HttpRequest(HttpMethodType.Get, url);
        request.Query = query;
        
        HttpResponse response = await _httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .ExecuteAction();
    }
    
    public async Task GetCreatorRoleType(long typeId, Action<RoleTypeResponse>? successAction = null, Action? notFoundAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Roles.GetCreatorRoleType, typeId);
        
        HttpRequest request = new HttpRequest(HttpMethodType.Get, url);
        
        HttpResponse response = await _httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor404NotFound(notFoundAction)
                .ExecuteAction();
    }
    
    public async Task PostCreatorRoleType(RoleTypeRequest data, Action<RoleTypeResponse>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Roles.PostCreatorRoleType);
        
        HttpRequest request = new HttpRequest(HttpMethodType.Post, url);
        request.Body = data;
        
        HttpResponse response = await _httpClientService.SendRequestAsync(request);
        response.RegisterActionFor2XXSuccess(successAction)
                .RegisterActionFor400BadRequest(badRequestAction)
                .RegisterActionFor401Unauthorized(unauthorizedAction)
                .RegisterActionFor403Forbidden(forbiddenAction)
                .ExecuteAction();
    }
    
    public async Task DeleteCreatorRoleType(long typeId, Action? successAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null)
    {
        string url = GetUrl(EndpointsConfiguration.Roles.DeleteCreatorRoleType, typeId);
        
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

    protected override string GetServiceBase() => EndpointsConfiguration.Roles.Base;

    #endregion
}