using Microsoft.AspNetCore.Components;
using WatchIt.DTO.Models.Controllers.Media.Medium.Response;
using WatchIt.Website.Clients;

namespace WatchIt.Website.Components.Panels.Pages.MediumEditPage;

public partial class HeaderPanel : Component
{
    #region SERVICES

    [Inject] public IMediaClient MediaClient { get; set; } = null!;
    [Inject] public NavigationManager NavigationManager { get; set; } = null!;

    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] public required BaseMediumResponse? Data { get; set; }
    [Parameter] public required NullType TypeIfNull { get; set; }
    
    #endregion
}