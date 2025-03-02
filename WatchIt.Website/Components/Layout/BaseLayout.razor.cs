using System.Drawing;
using Blazorise.Snackbar;
using Microsoft.AspNetCore.Components;
using Refit;
using WatchIt.DTO.Models.Controllers.Accounts.Account;
using WatchIt.DTO.Models.Controllers.Photos.Photo;
using WatchIt.DTO.Models.Controllers.Photos.PhotoBackground;
using WatchIt.Website.Clients;
using WatchIt.Website.Services.Authentication;

namespace WatchIt.Website.Components.Layout;

public partial class BaseLayout : LayoutComponentBase
{
    #region SERVICES

    [Inject] private IAuthenticationService AuthenticationService { get; set; } = null!;
    [Inject] private IPhotosClient PhotosClient { get; set; } = null!;
    [Inject] private IAccountsClient AccountsClient { get; set; } = null!;

    #endregion
    
    
    
    #region FIELDS

    private static readonly PhotoBackgroundResponse StaticBackgroundSettings = new PhotoBackgroundResponse()
    {
        IsUniversal = true,
        FirstGradientColor = Color.FromArgb(0xc6, 0x72, 0x1c),
        SecondGradientColor = Color.FromArgb(0x85, 0x20, 0x0c),
    };
    
    private PhotoResponse? _defaultBackground;
    
    #endregion



    #region PROPERTIES

    private PhotoResponse? _customBackground;
    public PhotoResponse? CustomBackground
    {
        get => _customBackground;
        set
        {
            _customBackground = value;
            StateHasChanged();
        }
    }
    public PhotoResponse? Background
    {
        get
        {
            if (CustomBackground?.Background is not null)
            {
                return CustomBackground;
            }
            return _defaultBackground?.Background is not null ? _defaultBackground : null;
        }
    }
    public PhotoBackgroundResponse BackgroundSettings => Background is null ? StaticBackgroundSettings : Background.Background!;

    public AccountResponse? AuthorizedAccount { get; private set; }
    public bool AuthorizationLoaded { get; private set; }
    
    public SnackbarStack SnackbarStack { get; set; } = null!;
        
    #endregion



    #region PUBLIC METHODS

    public async Task RefreshAuthorization()
    {
        long? accountId = await AuthenticationService.GetAccountIdAsync();
        if (accountId.HasValue)
        {
            IApiResponse<AccountResponse> accountResponse = await AccountsClient.GetAccount(accountId.Value, true);
            if (accountResponse.IsSuccessful)
            {
                AuthorizedAccount = accountResponse.Content;
            }
        }
        AuthorizationLoaded = true;
        StateHasChanged();
    }

    #endregion
    
    
    
    #region PRIVATE METHODS

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        if (firstRender)
        {
            await OnFirstRenderAsync();
        }
    }

    protected async Task OnFirstRenderAsync() => await Task.WhenAll(
    [
        GetBackground(),
        RefreshAuthorization(),
    ]);

    protected async Task GetBackground()
    {
        IApiResponse<PhotoResponse?> response = await PhotosClient.GetPhotoBackground();
        if (response.IsSuccessStatusCode)
        {
            _defaultBackground = response.Content;
        }
        StateHasChanged();
    }
    
    #endregion
}