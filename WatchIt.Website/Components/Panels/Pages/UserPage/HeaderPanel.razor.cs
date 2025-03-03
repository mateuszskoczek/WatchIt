using Microsoft.AspNetCore.Components;
using Refit;
using WatchIt.DTO.Models.Controllers.Accounts.Account;
using WatchIt.Website.Clients;
using WatchIt.Website.Services.Authentication;

namespace WatchIt.Website.Components.Panels.Pages.UserPage;

public partial class HeaderPanel : Component
{
    #region SERVICES

    [Inject] private IAuthenticationService AuthenticationService { get; set; } = default!;
    [Inject] private IAccountsClient AccountsClient { get; set; } = default!;

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
        string token = await AuthenticationService.GetRawAccessTokenAsync() ?? string.Empty;
        _followLoading = true;
        IApiResponse response;
        if (Followers.Any(x => x.Id == LoggedUserData!.Id))
        {
            response = await AccountsClient.DeleteAccountFollow(token, Data.Id);
            if (response.IsSuccessful)
            {
                Followers.RemoveAll(x => x.Id == LoggedUserData!.Id);
                FollowingChanged?.Invoke(false);
            }
        }
        else
        {
            response = await AccountsClient.PostAccountFollow(token, Data.Id);
            if (response.IsSuccessful)
            {
                Followers.Add(LoggedUserData);
                FollowingChanged?.Invoke(true);
            }
        }
        if (response.IsSuccessful)
        {
            _followLoading = false;
        }
    }

    #endregion
}