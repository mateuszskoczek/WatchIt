using System.Net;
using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model;
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

    private User? _user;

    private UserEditPageHeaderPanelComponent _header = default!;
    
    #endregion
    
    
    
    #region PRIVATE METHODS

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            Layout.BackgroundPhoto = null;
            
            _user = await AuthenticationService.GetUserAsync();
            if (_user is null)
            {
                NavigationManager.NavigateTo($"/auth?redirect_to={WebUtility.UrlEncode("/user/edit")}");
            }
            else
            {
                StateHasChanged();
            }
        }
    }
    
    private async Task PictureChanged() => await Task.WhenAll(
    [
        _header.ReloadPicture(),
        Layout.ReloadProfilePicture()
    ]);

    #endregion
}