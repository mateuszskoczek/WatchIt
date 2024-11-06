using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model.Accounts;
using WatchIt.Website.Components.Common.Subcomponents;
using WatchIt.Website.Services.Authentication;
using WatchIt.Website.Services.Client.Accounts;

namespace WatchIt.Website.Components.Pages.UserEditPage.Panels;

public partial class UserEditPageHeaderPanelComponent : ComponentBase
{
    #region SERVICES

    [Inject] public IAccountsClientService AccountsClientService { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;

    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] public required AccountResponse AccountData { get; set; }
    
    #endregion
    
    
    
    #region FIELDS

    private AccountPictureComponent _accountPicture = default!;
    
    #endregion



    #region PUBLIC METHODS

    public async Task ReloadPicture() => await _accountPicture.Reload();

    #endregion
}