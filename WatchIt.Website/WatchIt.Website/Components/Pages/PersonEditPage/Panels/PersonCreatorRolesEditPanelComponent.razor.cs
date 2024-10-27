using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model.Media;
using WatchIt.Common.Model.Roles;
using WatchIt.Website.Services.Client.Media;
using WatchIt.Website.Services.Client.Persons;
using WatchIt.Website.Services.Client.Roles;

namespace WatchIt.Website.Components.Pages.PersonEditPage.Panels;

public partial class PersonCreatorRolesEditPanelComponent : ComponentBase
{
    #region SERVICES
    
    [Inject] private IPersonsClientService PersonsClientService { get; set; } = default!;
    [Inject] private IMediaClientService MediaClientService { get; set; } = default!;
    [Inject] private IRolesClientService RolesClientService { get; set; } = default!;
    
    #endregion
    
    
    
    #region PARAMETERS

    [Parameter] public required long? Id { get; set; }
    [Parameter] public required Dictionary<long, MediaResponse> Media { get; set; }
    [Parameter] public string Class { get; set; } = string.Empty;

    #endregion
    
    
    
    #region FIELDS

    private bool _loaded;
    private string? _error;

    private Dictionary<Guid, (CreatorRoleResponse Data, bool Deleting)> _roles = [];
    private Dictionary<short, string> _roleTypes = [];


    private Guid? _editedId;
    private ICreatorRolePersonRequest? _editedModel;
    
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
                    PersonsClientService.GetPersonAllCreatorRoles(Id.Value, successAction: data => _roles = data.ToDictionary(x => x.Id, x => (x, false))),
                    RolesClientService.GetAllCreatorRoleTypes(successAction: data => _roleTypes = data.ToDictionary(x => x.Id, x => x.Name)),
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
        void SuccessPost(CreatorRoleResponse data)
        {
            _roles[data.Id] = (data, false);
            _roles = _roles.OrderBy(x => Media.First(y => y.Key == x.Value.Data.MediaId).Value.ReleaseDate).ToDictionary(x => x.Key, x => x.Value);
            
            _saving = false;
            _editingMode = false;
        }

        void SuccessPut()
        {
            CreatorRoleResponse temp = _roles[_editedId!.Value].Data;
            temp.MediaId = _editedModel.MediaId;
            temp.TypeId = _editedModel.TypeId;
            
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
            await RolesClientService.PutCreatorRole(_editedId.Value, _editedModel as CreatorRoleUniversalRequest, SuccessPut, BadRequest, Unauthorized);
        }
        else
        {
            await PersonsClientService.PostPersonCreatorRole(Id!.Value, _editedModel as CreatorRolePersonRequest, SuccessPost, BadRequest, Unauthorized);
        }
    }

    private void ActivateEdit(Guid? id = null)
    {
        _editedId = id;
        _editedModel = id.HasValue ? new CreatorRoleUniversalRequest(_roles[id.Value].Data) : new CreatorRolePersonRequest()
        {
            TypeId = _roleTypes.Keys.First()
        };
        _editingMode = true;
    }

    private async Task Delete(Guid id)
    {
        _roles[id] = (_roles[id].Data, true);
        await RolesClientService.DeleteCreatorRole(id, () => _roles.Remove(id));
    }

    #endregion
}