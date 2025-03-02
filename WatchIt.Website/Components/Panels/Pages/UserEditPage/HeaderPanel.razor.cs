using Microsoft.AspNetCore.Components;
using WatchIt.DTO.Models.Controllers.Accounts.Account;
using WatchIt.Website.Clients;
using WatchIt.Website.Components.Subcomponents.Common;

namespace WatchIt.Website.Components.Panels.Pages.UserEditPage;

public partial class HeaderPanel : Component
{
    #region SERVICES

    [Inject] public NavigationManager NavigationManager { get; set; } = null!;

    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] public required AccountResponse Data { get; set; }
    
    #endregion
}