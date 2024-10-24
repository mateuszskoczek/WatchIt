using Microsoft.AspNetCore.Components;
using WatchIt.Website.Services.WebAPI.Media;
using WatchIt.Website.Services.WebAPI.Persons;
using WatchIt.Website.Services.WebAPI.Roles;

namespace WatchIt.Website.Components.Pages.PersonPage.Panels;

public partial class PersonActorRolesPanelComponent : ComponentBase
{
    #region SERVICES
    
    [Inject] private IMediaWebAPIService MediaWebAPIService { get; set; } = default!;
    [Inject] private IPersonsWebAPIService PersonsWebAPIService { get; set; } = default!;
    [Inject] private IRolesWebAPIService RolesWebAPIService { get; set; } = default!;
    
    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] public string Class { get; set; } = string.Empty;
    [Parameter] public required long Id { get; set; }
    [Parameter] public Action? OnRatingChanged { get; set; }
    
    #endregion
}