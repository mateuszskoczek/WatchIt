using System.Net;
using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model.Accounts;
using WatchIt.Common.Model.Photos;
using WatchIt.Website.Services.Utility.Authentication;
using WatchIt.Website.Services.Utility.Tokens;
using WatchIt.Website.Services.WebAPI.Accounts;
using WatchIt.Website.Services.WebAPI.Media;
using WatchIt.Website.Services.WebAPI.Photos;

namespace WatchIt.Website.Layout;

public partial class MainLayout : LayoutComponentBase
{
    #region SERVICES
    
    [Inject] public ILogger<MainLayout> Logger { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;
    [Inject] public ITokensService TokensService { get; set; } = default!;
    [Inject] public IAuthenticationService AuthenticationService { get; set; } = default!;
    [Inject] public IAccountsWebAPIService AccountsWebAPIService { get; set; } = default!;
    [Inject] public IMediaWebAPIService MediaWebAPIService { get; set; } = default!;
    [Inject] public IPhotosWebAPIService PhotosWebAPIService { get; set; } = default!;
    
    #endregion
    
    
    
    #region FIELDS

    private bool _loaded;
    
    private User? _user;
    private PhotoResponse? _defaultBackgroundPhoto;
    private AccountProfilePictureResponse? _userProfilePicture;
    
    private bool _searchbarVisible;
    private string _searchbarText = string.Empty;
    
    #endregion
    
    
    
    #region PROPERTIES

    private PhotoResponse? _backgroundPhoto;
    public PhotoResponse? BackgroundPhoto
    {
        get => _backgroundPhoto;
        set
        {
            _backgroundPhoto = value;
            StateHasChanged();
        }
    }
    
    #endregion
    
    
    
    #region PRIVATE METHODS
    
    #region Main
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            List<Task> endTasks = new List<Task>();
            List<Task> step1Tasks = new List<Task>();
            
            // STEP 0
            step1Tasks.AddRange(
            [
                Task.Run(async () => _user = await AuthenticationService.GetUserAsync())
            ]);
            endTasks.AddRange(
            [
                PhotosWebAPIService.GetPhotoRandomBackground(data => _defaultBackgroundPhoto = data)
            ]);
            
            // STEP 1
            await Task.WhenAll(step1Tasks);
            if (_user is not null)
            {
                endTasks.AddRange(
                [
                    AccountsWebAPIService.GetAccountProfilePicture(_user.Id, data => _userProfilePicture = data)
                ]);
            }
            
            // END
            await Task.WhenAll(endTasks);
            
            _loaded = true;
            StateHasChanged();
        }
    }

    private PhotoResponse? GetBackgroundPhoto()
    {
        if (BackgroundPhoto?.Background is not null)
        {
            return BackgroundPhoto;
        }
        else if (_defaultBackgroundPhoto?.Background is not null)
        {
            return _defaultBackgroundPhoto;
        }
        return null;
    }
    
    #endregion
    
    #region Search

    private void SearchStart()
    {
        if (!string.IsNullOrWhiteSpace(_searchbarText))
        {
            string query = WebUtility.UrlEncode(_searchbarText);
            NavigationManager.NavigateTo($"/search/{query}");
        }
    }
    
    #endregion
    
    #region User menu
    
    private async Task UserMenuLogOut()
    {
        await AuthenticationService.LogoutAsync();
        await TokensService.RemoveAuthenticationData();
        NavigationManager.Refresh(true);
    }
    
    #endregion
    
    #endregion
}