using System.Net;
using Microsoft.AspNetCore.Components;
using Refit;
using WatchIt.DTO.Models.Controllers.Accounts.Account;
using WatchIt.DTO.Models.Controllers.Photos.Photo;
using WatchIt.DTO.Models.Generics.Image;
using WatchIt.Website.Clients;
using WatchIt.Website.Components.Layout;
using WatchIt.Website.Services.Authentication;

namespace WatchIt.Website.Components.Pages;

public partial class UserEditPage : Page
{
    #region SERVICES
    
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private IAuthenticationService AuthenticationService { get; set; } = null!;
    [Inject] private IAccountsClient AccountsClient { get; set; } = null!;
    
    #endregion
    
    
    
    #region FIELDS

    private AccountResponse? _data;

    //private HeaderPanel _header = default!;
    
    #endregion
    
    
    
    #region PRIVATE METHODS

    protected override async Task OnFirstRenderAsync()
    {
        await base.OnFirstRenderAsync();

        while (!Base.AuthorizationLoaded)
        {
            await Task.Delay(10);
        }
        
        if (Base.AuthorizedAccount is null)
        {
            NavigationManager.NavigateTo($"/auth?redirect_to={WebUtility.UrlEncode("/user_settings")}");
            return;
        }
        StateHasChanged();
        
        IApiResponse<PhotoResponse> backgroundResponse = await AccountsClient.GetAccountBackgroundPicture(Base.AuthorizedAccount.Id);
        if (backgroundResponse.IsSuccessful)
        {
            Base.CustomBackground = backgroundResponse.Content;
        }
        StateHasChanged();
    }

    #endregion
}