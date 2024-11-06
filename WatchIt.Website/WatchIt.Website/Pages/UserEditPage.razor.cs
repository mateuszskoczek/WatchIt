using System.Net;
using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model;
using WatchIt.Common.Model.Accounts;
using WatchIt.Common.Model.Photos;
using WatchIt.Website.Components.Pages.UserEditPage.Panels;
using WatchIt.Website.Layout;
using WatchIt.Website.Services.Authentication;
using WatchIt.Website.Services.Client.Accounts;

namespace WatchIt.Website.Pages;

public partial class UserEditPage : ComponentBase
{
    #region SERVICES
    
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;
    [Inject] private IAuthenticationService AuthenticationService { get; set; } = default!;
    [Inject] private IAccountsClientService AccountsClientService { get; set; } = default!;
    
    #endregion 
    
    
    
    #region PARAMETERS
    
    [CascadingParameter] public MainLayout Layout { get; set; } = default!;
    
    #endregion
    
    
    
    #region FIELDS

    private AccountResponse? _accountData;

    private UserEditPageHeaderPanelComponent _header = default!;
    
    #endregion
    
    
    
    #region PRIVATE METHODS

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            Layout.BackgroundPhoto = null;
            
            User? user = await AuthenticationService.GetUserAsync();
            if (user is null)
            {
                NavigationManager.NavigateTo($"/auth?redirect_to={WebUtility.UrlEncode("/user/edit")}");
                return;
            }
            StateHasChanged();
            
            await Task.WhenAll(
            [
                AccountsClientService.GetAccount(user.Id, data => _accountData = data),
                AccountsClientService.GetAccountProfileBackground(user.Id, data => Layout.BackgroundPhoto = data)
            ]);
            StateHasChanged();
        }
    }
    
    private async Task PictureChanged() => await Task.WhenAll(
    [
        _header.ReloadPicture(),
        Layout.ReloadProfilePicture()
    ]);
    
    private void BackgroundChanged(PhotoResponse? background)
    {
        Layout.BackgroundPhoto = background;
    }

    #endregion
}