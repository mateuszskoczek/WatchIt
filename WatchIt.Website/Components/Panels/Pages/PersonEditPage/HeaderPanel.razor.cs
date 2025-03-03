using Microsoft.AspNetCore.Components;
using WatchIt.DTO.Models.Controllers.People.Person;
using WatchIt.Website.Clients;

namespace WatchIt.Website.Components.Panels.Pages.PersonEditPage;

public partial class HeaderPanel : Component
{
    #region SERVICES

    [Inject] public IPeopleClient PeopleClient { get; set; } = null!;
    [Inject] public NavigationManager NavigationManager { get; set; } = null!;

    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] public required PersonResponse? Data { get; set; }
    
    #endregion
}