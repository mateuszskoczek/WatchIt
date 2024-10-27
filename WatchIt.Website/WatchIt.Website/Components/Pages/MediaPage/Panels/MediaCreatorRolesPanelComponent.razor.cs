using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model.Persons;
using WatchIt.Common.Model.Roles;
using WatchIt.Website.Components.Common.Subcomponents;
using WatchIt.Website.Services.Authentication;
using WatchIt.Website.Services.Client.Media;
using WatchIt.Website.Services.Client.Persons;
using WatchIt.Website.Services.Client.Roles;

namespace WatchIt.Website.Components.Pages.MediaPage.Panels;

public partial class MediaCreatorRolesPanelComponent : ComponentBase
{
    #region SERVICES

    [Inject] private IPersonsClientService PersonsClientService { get; set; } = default!;
    [Inject] private IMediaClientService MediaClientService { get; set; } = default!;
    [Inject] private IRolesClientService RolesClientService { get; set; } = default!;
    
    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] public string Class { get; set; } = string.Empty;
    [Parameter] public required long Id { get; set; }
    
    #endregion



    #region FIELDS

    private RoleListComponent<CreatorRoleResponse, CreatorRoleMediaQueryParameters, PersonResponse> _roleListComponent;

    private bool _loaded;

    private IEnumerable<RoleTypeResponse> _roleTypes;
    private CreatorRoleMediaQueryParameters _query;

    #endregion



    #region PRIVATE METHODS

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            List<Task> endTasks = new List<Task>();
            
            // STEP 0
            endTasks.AddRange(
            [
                RolesClientService.GetAllCreatorRoleTypes(successAction: data => _roleTypes = data)
            ]);
            
            // END
            await Task.WhenAll(endTasks);
            _query = new CreatorRoleMediaQueryParameters { TypeId = _roleTypes.First().Id };
            
            _loaded = true;
            StateHasChanged();
        }
    }

    private async Task CheckedTypeChanged(short value)
    {
        _query.TypeId = value;
        await _roleListComponent.Refresh();
    }

    #endregion
}