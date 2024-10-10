using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model.Media;
using WatchIt.Common.Model.Roles;
using WatchIt.Website.Services.WebAPI.Media;
using WatchIt.Website.Services.WebAPI.Persons;
using WatchIt.Website.Services.WebAPI.Roles;

namespace WatchIt.Website.Components.PersonEditPage;

public partial class PersonRolesEditActorComponent : ComponentBase
{
    #region SERVICES
    
    [Inject] private IPersonsWebAPIService PersonsWebAPIService { get; set; } = default!;
    [Inject] private IMediaWebAPIService MediaWebAPIService { get; set; } = default!;
    [Inject] private IRolesWebAPIService RolesWebAPIService { get; set; } = default!;
    
    #endregion
    
    
    
    #region PARAMETERS

    [Parameter] public required long? Id { get; set; }
    [Parameter] public required Dictionary<long, MediaResponse> Media { get; set; }
    [Parameter] public string Class { get; set; } = string.Empty;

    #endregion
    
    
    
    #region FIELDS

    private bool _loaded;
    private string? _error;

    private Dictionary<Guid, (ActorRoleResponse Data, bool Deleting)> _roles = [];
    private Dictionary<short, string> _roleTypes = [];


    private Guid? _editedId;
    private IActorRolePersonRequest? _editedModel;
    
    private bool _editingMode;
    private bool _saving;
    
    #endregion
    
    
    
    #region PUBLIC METHODS

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            List<Task> endTasks = new List<Task>();
            
            // STEP 0
            if (Id.HasValue)
            {
                endTasks.AddRange(
                [
                    PersonsWebAPIService.GetPersonAllActorRoles(Id.Value, successAction: data => _roles = data.ToDictionary(x => x.Id, x => (x, false))),
                    RolesWebAPIService.GetAllActorRoleTypes(successAction: data => _roleTypes = data.ToDictionary(x => x.Id, x => x.Name)),
                ]);
            }

            // END
            await Task.WhenAll(endTasks);
            _roles = _roles.OrderBy(x => Media.First(y => y.Key == x.Value.Data.MediaId).Value.ReleaseDate).ToDictionary(x => x.Key, x => x.Value);
            
            _loaded = true;
            StateHasChanged();
        }
    }

    private void CancelEdit()
    {
        _error = null;
        _editingMode = false;
    }
    
    private async Task SaveEdit()
    {
        void SuccessPost(ActorRoleResponse data)
        {
            _roles[data.Id] = (data, false);
            _roles = _roles.OrderBy(x => Media.First(y => y.Key == x.Value.Data.MediaId).Value.ReleaseDate).ToDictionary(x => x.Key, x => x.Value);
            
            _saving = false;
            _editingMode = false;
        }

        void SuccessPut()
        {
            ActorRoleResponse temp = _roles[_editedId!.Value].Data;
            temp.MediaId = _editedModel.MediaId;
            temp.TypeId = _editedModel.TypeId;
            temp.Name = _editedModel.Name;
            
            _roles[_editedId!.Value] = (temp, false);
            _roles = _roles.OrderBy(x => Media.First(y => y.Key == x.Value.Data.MediaId).Value.ReleaseDate).ToDictionary(x => x.Key, x => x.Value);

            _saving = false;
            _editingMode = false;
        }

        void BadRequest(IDictionary<string, string[]> errors)
        {
            _error = errors.SelectMany(x => x.Value).FirstOrDefault() ?? "Unknown error";
            _saving = false;
        }

        void Unauthorized()
        {
            _error = "You do not have permission to do this";
            _saving = false;
        }
        
        _error = null;
        _saving = true;
        if (_editedId.HasValue)
        {
            await RolesWebAPIService.PutActorRole(_editedId.Value, _editedModel as ActorRoleUniversalRequest, SuccessPut, BadRequest, Unauthorized);
        }
        else
        {
            await PersonsWebAPIService.PostPersonActorRole(Id!.Value, _editedModel as ActorRolePersonRequest, SuccessPost, BadRequest, Unauthorized);
        }
    }

    private void ActivateEdit(Guid? id = null)
    {
        _editedId = id;
        _editedModel = id.HasValue ? new ActorRoleUniversalRequest(_roles[id.Value].Data) : new ActorRolePersonRequest()
        {
            TypeId = _roleTypes.Keys.First()
        };
        _editingMode = true;
    }

    private async Task Delete(Guid id)
    {
        _roles[id] = (_roles[id].Data, true);
        await RolesWebAPIService.DeleteActorRole(id, () => _roles.Remove(id));
    }

    #endregion
}