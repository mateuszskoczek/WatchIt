using Microsoft.AspNetCore.Components;
using WatchIt.Website.Clients;

namespace WatchIt.Website.Components.Pages;

public partial class HomePage : Page
{
    #region SERVICES

    [Inject] public NavigationManager NavigationManager { get; set; } = default!;
    [Inject] public IMediaClient MediaClient { get; set; } = default!;
    [Inject] public IPeopleClient PeopleClient { get; set; } = default!;
    
    #endregion
}