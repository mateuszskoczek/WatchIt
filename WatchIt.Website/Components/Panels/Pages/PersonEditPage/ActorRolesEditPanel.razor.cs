using System.Net;
using Blazorise.Snackbar;
using Microsoft.AspNetCore.Components;
using Refit;
using WatchIt.DTO.Models.Controllers.Media.Medium.Response;
using WatchIt.DTO.Models.Controllers.People.Person;
using WatchIt.DTO.Models.Controllers.Roles.Role.Query;
using WatchIt.DTO.Models.Controllers.Roles.Role.Request;
using WatchIt.DTO.Models.Controllers.Roles.Role.Response;
using WatchIt.DTO.Models.Controllers.Roles.RoleActorType;
using WatchIt.DTO.Query;
using WatchIt.Website.Clients;
using WatchIt.Website.Components.Layout;
using WatchIt.Website.Services.Authentication;

namespace WatchIt.Website.Components.Panels.Pages.PersonEditPage;

public partial class ActorRolesEditPanel : Component
{
    #region SERVICES

    [Inject] private IAuthenticationService AuthenticationService { get; set; } = null!;
    [Inject] private IMediaClient MediaClient { get; set; } = null!;
    [Inject] private IRolesClient RolesClient { get; set; } = null!;
    
    #endregion



    #region PARAMETERS
    
    [Parameter] public required PersonResponse Data { get; set; }
    [Parameter] public List<MediumResponse>? Media { get; set; }
    
    [Parameter] public bool Disabled { get; set; }
    [Parameter] public string Class { get; set; } = string.Empty;

    #endregion



    #region FIELDS

    private bool _loaded;
    
    private bool _editingMode;
    private Guid? _editedId;
    private RoleActorRequest _roleRequest = null!;
    private bool _saving;
    
    private Dictionary<long, MediumResponse> _mediaDict = new Dictionary<long, MediumResponse>();
    private Dictionary<short, string> _roleTypes = new Dictionary<short, string>();
    private Dictionary<Guid, (RoleActorResponse Data, bool Deleting)> _roles = new Dictionary<Guid, (RoleActorResponse Data, bool Deleting)>();

    #endregion



    #region PRIVATE METHODS

    protected override async Task OnFirstRenderAsync()
    {
        await base.OnFirstRenderAsync();

        ResetRequest();

        await Task.WhenAll(
        [
            LoadRoleTypes(),
            LoadMedia()
        ]);
        if (Data is not null)
        {
            await LoadRoles();
        }
        
        _loaded = true;
        StateHasChanged();
    }

    private async Task LoadRoleTypes()
    {
        IApiResponse<IEnumerable<RoleActorTypeResponse>> response = await RolesClient.GetRoleActorTypes();
        if (response.IsSuccessful)
        {
            _roleTypes = response.Content.ToDictionary(x => x.Id, x => x.Name);
        }
        else
        {
            await Base.SnackbarStack.PushAsync("An error occured. Actor role types list could not be obtained.", SnackbarColor.Danger);
        }
    }

    private async Task LoadMedia()
    {
        IEnumerable<MediumResponse>? media = Media;
        if (media is null)
        {
            IApiResponse<IEnumerable<MediumResponse>> response = await MediaClient.GetMedia(includePictures: true);
            if (response.IsSuccessful)
            {
                media = response.Content;
            }
            else
            {
                await Base.SnackbarStack.PushAsync("An error occured. Media list could not be obtained.", SnackbarColor.Danger);
                return;
            }
        }
        _mediaDict = media.ToDictionary(x => x.Id, x => x);
    }

    private async Task LoadRoles()
    {
        RoleActorFilterQuery filter = new RoleActorFilterQuery
        {
            PersonId = Data.Id
        };
        OrderQuery order = new OrderQuery
        {
            OrderBy = "medium.release_date",
        };
        IApiResponse<IEnumerable<RoleActorResponse>> response = await RolesClient.GetRoleActors(filter, order);
        if (response.IsSuccessful)
        {
            _roles = response.Content.ToDictionary(x => x.Id, x => (x, false));
        }
        else
        {
            await Base.SnackbarStack.PushAsync("An error occured. Actor roles could not be obtained.", SnackbarColor.Danger);
        }
    }
    
    
    private void CancelEditData()
    {
        _editingMode = false;
    }

    private async Task SaveData()
    {
        string token = await AuthenticationService.GetRawAccessTokenAsync() ?? string.Empty;
        _saving = true;

        IApiResponse<RoleActorResponse> response = await (_editedId.HasValue switch
        {
            true => RolesClient.PutRoleActor(token, _editedId.Value, _roleRequest),
            false => RolesClient.PostRoleActor(token, _roleRequest),
        });
        switch (response)
        {
            case { IsSuccessful: true }:
                _roles[response.Content.Id] = (response.Content, false);
                _roles = _roles.OrderBy(x => _mediaDict[x.Value.Data.MediumId].ReleaseDate)
                               .ToDictionary(x => x.Key, x => x.Value);
                await Base.SnackbarStack.PushAsync("Role saved successfully.", SnackbarColor.Success); 
                break;
            case { StatusCode: HttpStatusCode.Forbidden } or { StatusCode: HttpStatusCode.Unauthorized }:
                await Base.SnackbarStack.PushAsync("You are not authorized to edit roles data.", SnackbarColor.Danger);
                break;
            case { StatusCode: HttpStatusCode.BadRequest }:
                string? content = "An unknown error occured.";
                if (response.Error is ValidationApiException ex)
                {
                    string? exContent = ex.Content?.Errors.SelectMany(x => x.Value).FirstOrDefault();
                    if (exContent is not null)
                    {
                        content = exContent;
                    }
                }
                await Base.SnackbarStack.PushAsync(content, SnackbarColor.Danger);
                break;
            default:
                await Base.SnackbarStack.PushAsync("An unknown error occured.", SnackbarColor.Danger);
                break;
        }
        
        _saving = false;
        _editingMode = false;
    }
    
    private void ActivateEditData(Guid? id = null)
    {
        _editedId = id;
        ResetRequest();
        if (id is not null && _roles.TryGetValue(id.Value, out (RoleActorResponse Data, bool Deleting) role))
        {
            _roleRequest.Name = role.Data.Name;
            _roleRequest.MediumId = role.Data.MediumId;
            _roleRequest.TypeId = role.Data.TypeId;
        }
        _editingMode = true;
    }
    
    private async Task DeleteData(Guid id)
    {
        _roles[id] = (_roles[id].Data, true);
        
        string token = await AuthenticationService.GetRawAccessTokenAsync() ?? string.Empty;
        IApiResponse response = await RolesClient.DeleteRole(token, id);
        switch (response)
        {
            case {IsSuccessful: true}:
                _roles.Remove(id);
                await Base.SnackbarStack.PushAsync("Role removed successfully.", SnackbarColor.Success);
                break;
            case {StatusCode: HttpStatusCode.Forbidden} or {StatusCode: HttpStatusCode.Unauthorized}:
                await Base.SnackbarStack.PushAsync("You are not authorized to remove roles.", SnackbarColor.Danger);
                break;
            default:
                await Base.SnackbarStack.PushAsync("An unknown error occured.", SnackbarColor.Danger);
                break;
        }
    }

    private void ResetRequest() => _roleRequest = Data is null ? new RoleActorRequest() : new RoleActorRequest
    {
        PersonId = Data.Id,
    };

    #endregion
}