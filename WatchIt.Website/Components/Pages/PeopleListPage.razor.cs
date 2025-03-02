using Microsoft.AspNetCore.Components;
using WatchIt.Website.Clients;
using WatchIt.Website.Components.Layout;
using WatchIt.Website.Services.Authentication;

namespace WatchIt.Website.Components.Pages;

public partial class PeopleListPage : Page
{
    #region SERVICES
    
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private IAuthenticationService AuthenticationService { get; set; } = null!;
    [Inject] private IPeopleClient PeopleClient { get; set; } = null!;
    
    #endregion
}