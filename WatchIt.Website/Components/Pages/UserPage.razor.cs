using System.Net;
using Blazorise.Snackbar;
using Microsoft.AspNetCore.Components;
using Refit;
using WatchIt.DTO.Models.Controllers.Accounts.Account;
using WatchIt.DTO.Models.Controllers.Photos.Photo;
using WatchIt.Website.Clients;
using WatchIt.Website.Components.Layout;
using WatchIt.Website.Services.Authentication;

namespace WatchIt.Website.Components.Pages;

public partial class UserPage : Page
{
    #region SERVICES
    
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private IAuthenticationService AuthenticationService { get; set; } = null!;
    [Inject] private IAccountsClient AccountsClient { get; set; } = null!;
    [Inject] private IMediaClient MediaClient { get; set; } = null!;
    [Inject] private IPeopleClient PeopleClient { get; set; } = null!;
    
    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] public long? Id { get; set; }
    
    #endregion
    
    
    
    #region FIELDS

    private bool _loaded;
    private bool _redirection;
    private bool _owner;

    private AccountResponse? _data;
    private List<AccountResponse> _followers;
    
    #endregion
    
    
    
    #region PRIVATE METHODS

    protected override async Task OnFirstRenderAsync()
    {
        await LoadUserData();
        if (_data is not null)
        {
            await Task.WhenAll(
            [
                LoadProfileBackground(),
                LoadFollowers()
            ]);
        }
        
        _loaded = !_redirection;
        StateHasChanged();
    }
    
    private async Task LoadUserData()
    {
        if (Id.HasValue)
        {
            IApiResponse<AccountResponse> response = await AccountsClient.GetAccount(Id.Value);
            if (!response.IsSuccessful)
            {
                await Base.SnackbarStack.PushAsync("An error occured. Account info could not be obtained.", SnackbarColor.Danger);
            }
            else
            {
                _data = response.Content;
            }
        }
        else
        {
            while (!Base.AuthorizationLoaded)
            {
                await Task.Delay(10);
            }
            
            if (Base.AuthorizedAccount is null)
            {
                NavigationManager.NavigateTo($"/auth?redirect_to={WebUtility.UrlEncode("/user")}");
                _redirection = true;
                return;
            }
            
            Id = Base.AuthorizedAccount.Id;
            _data = Base.AuthorizedAccount;
        }
        _owner = Id.Value == Base.AuthorizedAccount?.Id;
    }

    private async Task LoadProfileBackground()
    {
        IApiResponse<PhotoResponse> response = await AccountsClient.GetAccountBackgroundPicture(_data!.Id);
        if (response.IsSuccessful)
        {
            Base.CustomBackground = response.Content;
        }
        else if (response.StatusCode != HttpStatusCode.NotFound)
        {
            await Base.SnackbarStack.PushAsync("An error occured. Profile background loading failed.", SnackbarColor.Danger);
        }
    }

    private async Task LoadFollowers()
    {
        IApiResponse<IEnumerable<AccountResponse>> response = await AccountsClient.GetAccountFollowers(_data!.Id);
        if (response.IsSuccessful)
        {
            _followers = response.Content.ToList();
        }
        else
        {
            await Base.SnackbarStack.PushAsync("An error occured. Followers could not be obtained.", SnackbarColor.Danger);
        }
    }
    
    #endregion
}