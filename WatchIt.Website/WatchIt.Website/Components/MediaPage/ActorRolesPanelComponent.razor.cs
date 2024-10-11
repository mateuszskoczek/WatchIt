using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model.Roles;
using WatchIt.Website.Services.Utility.Configuration;
using WatchIt.Website.Services.WebAPI.Media;

namespace WatchIt.Website.Components.MediaPage;

public partial class ActorRolesPanelComponent : ComponentBase
{
    #region SERVICES
    
    [Inject] private IMediaWebAPIService MediaWebAPIService { get; set; } = default!;
    
    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] public string Class { get; set; } = string.Empty;
    [Parameter] public required long Id { get; set; }
    
    #endregion
    
    
    
    #region FIELDS

    private bool _loaded;

    private IEnumerable<ActorRoleResponse> _roles = [];

    #endregion

    
    
    #region PUBLIC METHODS

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        List<Task> endTasks = new List<Task>();
        
        // STEP 0
        endTasks.AddRange(
        [
            MediaWebAPIService.GetMediaAllActorRoles(Id, successAction: data => _roles = data)
        ]);
        
        // END
        await Task.WhenAll(endTasks);
        
        _loaded = true;
        StateHasChanged();
    }

    #endregion
}