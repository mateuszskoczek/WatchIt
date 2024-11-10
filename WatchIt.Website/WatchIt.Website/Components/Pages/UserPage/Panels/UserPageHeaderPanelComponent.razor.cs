using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model.Accounts;
using WatchIt.Website.Services.Authentication;
using WatchIt.Website.Services.Client.Accounts;

namespace WatchIt.Website.Components.Pages.UserPage.Panels;

public partial class UserPageHeaderPanelComponent : ComponentBase
{
    #region SERVICES

    [Inject] private IAccountsClientService AccountsClientService { get; set; } = default!;

    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] public required AccountResponse Data { get; set; }
    [Parameter] public required List<AccountResponse> Followers { get; set; }
    [Parameter] public AccountResponse? LoggedUserData { get; set; }
    [Parameter] public Action<bool>? FollowingChanged { get; set; }
    
    #endregion
    
    
    
    #region FIELDS

    private bool _followLoading;
    
    #endregion
    
    
    
    #region PRIVATE METHODS

    private async Task Follow()
    {
        _followLoading = true;
        if (Followers.Any(x => x.Id == LoggedUserData!.Id))
        {
            await AccountsClientService.DeleteAccountFollow(Data.Id, () =>
            {
                Followers.RemoveAll(x => x.Id == LoggedUserData!.Id);
                FollowingChanged?.Invoke(false);
                _followLoading = false;
            });
        }
        else
        {
            await AccountsClientService.PostAccountFollow(Data.Id, () =>
            {
                Followers.Add(LoggedUserData);
                FollowingChanged?.Invoke(true);
                _followLoading = false;
            });
        }
    }

    #endregion
}