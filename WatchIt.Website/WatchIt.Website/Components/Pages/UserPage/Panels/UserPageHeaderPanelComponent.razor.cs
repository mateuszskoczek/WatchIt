using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model.Accounts;
using WatchIt.Website.Services.Authentication;
using WatchIt.Website.Services.Client.Accounts;

namespace WatchIt.Website.Components.Pages.UserPage.Panels;

public partial class UserPageHeaderPanelComponent : ComponentBase
{
    #region SERVICES

    [Inject] private IAuthenticationService AuthenticationService { get; set; } = default!;
    [Inject] private IAccountsClientService AccountsClientService { get; set; } = default!;

    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] public required AccountResponse AccountData { get; set; }
    
    #endregion
    
    
    
    #region FIELDS
    
    private AccountProfilePictureResponse? _accountProfilePicture;

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
                AccountsClientService.GetAccountProfilePicture(AccountData.Id, data => _accountProfilePicture = data),
            ]);
            
            // END
            await Task.WhenAll(endTasks);
            
            StateHasChanged();
        }
    }

    #endregion
}