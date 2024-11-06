using System.Net;
using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model.Accounts;
using WatchIt.Common.Model.Photos;
using WatchIt.Website.Components.Common.Subcomponents;
using WatchIt.Website.Services.Authentication;
using WatchIt.Website.Services.Tokens;
using WatchIt.Website.Services.Client.Accounts;
using WatchIt.Website.Services.Client.Media;
using WatchIt.Website.Services.Client.Photos;

namespace WatchIt.Website.Layout;

public partial class MainLayout : LayoutComponentBase
{
    #region SERVICES
    
    [Inject] public ILogger<MainLayout> Logger { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;
    [Inject] public ITokensService TokensService { get; set; } = default!;
    [Inject] public IAuthenticationService AuthenticationService { get; set; } = default!;
    [Inject] public IMediaClientService MediaClientService { get; set; } = default!;
    [Inject] public IPhotosClientService PhotosClientService { get; set; } = default!;
    [Inject] public IAccountsClientService AccountsClientService { get; set; } = default!;
    
    #endregion
    
    
    
    #region FIELDS

    private AccountPictureComponent? _profilePicture;

    private bool _loaded;
    
    private User? _user;
    private AccountResponse? _accountData;
    private PhotoResponse? _defaultBackgroundPhoto;
    
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



    #region PUBLIC METHODS

    public async Task ReloadProfilePicture()
    {
        if (_profilePicture is not null)
        {
            await _profilePicture.Reload();
        }
    }

    #endregion
    
    
    
    #region PRIVATE METHODS
    
    #region Main
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await Task.WhenAll(
            [
                Task.Run(async () => _user = await AuthenticationService.GetUserAsync()),
                PhotosClientService.GetPhotoRandomBackground(data => _defaultBackgroundPhoto = data)
            ]);

            if (_user is not null)
            {
                await AccountsClientService.GetAccount(_user.Id, data => _accountData = data);
            }
            
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