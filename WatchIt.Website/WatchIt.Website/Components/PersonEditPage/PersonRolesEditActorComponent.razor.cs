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

    private Dictionary<Guid, (ActorRoleResponse Data, bool Deleting)> _roles = [];
    private Dictionary<short, string> _roleTypes = [];


    private Guid? _editedId;
    private IActorRolePersonRequest _editedModel = new ActorRoleRequest();
    
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
            
            _loaded = true;
            StateHasChanged();
        }
    }

    private void CancelEdit()
    {
        _editingMode = false;
    }
    
    private void SaveEdit()
    {
        
    }

    private void ActivateEdit(Guid? id = null)
    {
        _editedId = id;
        _editedModel = id.HasValue ? new ActorRoleRequest(_roles[id.Value].Data) : new ActorRoleRequest();
        _editingMode = true;
    }

    private async Task Delete(Guid id)
    {
        _roles[id] = (_roles[id].Data, true);
        await RolesWebAPIService.DeleteActorRole(id, () => _roles.Remove(id));
    }

    #endregion
}