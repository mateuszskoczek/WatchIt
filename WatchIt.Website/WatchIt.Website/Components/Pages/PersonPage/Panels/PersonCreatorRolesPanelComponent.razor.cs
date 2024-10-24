using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model.Media;
using WatchIt.Common.Model.Roles;
using WatchIt.Website.Components.Common.Subcomponents;
using WatchIt.Website.Services.WebAPI.Media;
using WatchIt.Website.Services.WebAPI.Persons;
using WatchIt.Website.Services.WebAPI.Roles;

namespace WatchIt.Website.Components.Pages.PersonPage.Panels;

public partial class PersonCreatorRolesPanelComponent : ComponentBase
{
    #region SERVICES

    [Inject] private IPersonsWebAPIService PersonsWebAPIService { get; set; } = default!;
    [Inject] private IMediaWebAPIService MediaWebAPIService { get; set; } = default!;
    [Inject] private IRolesWebAPIService RolesWebAPIService { get; set; } = default!;
    
    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] public string Class { get; set; } = string.Empty;
    [Parameter] public required long Id { get; set; }
    [Parameter] public Action? OnRatingChanged { get; set; }
    
    #endregion



    #region FIELDS

    private RoleListComponent<CreatorRoleResponse, CreatorRolePersonQueryParameters, MediaResponse> _roleListComponent;

    private bool _loaded;

    private IEnumerable<RoleTypeResponse> _roleTypes;
    private CreatorRolePersonQueryParameters _query;

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
                RolesWebAPIService.GetAllCreatorRoleTypes(successAction: data => _roleTypes = data)
            ]);
            
            // END
            await Task.WhenAll(endTasks);
            _query = new CreatorRolePersonQueryParameters { TypeId = _roleTypes.First().Id };
            
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