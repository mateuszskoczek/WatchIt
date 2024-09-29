using System.Net;
using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model.Accounts;
using WatchIt.Common.Model.Media;
using WatchIt.Common.Model.Photos;
using WatchIt.Website.Services.Utility.Authentication;
using WatchIt.Website.Services.Utility.Tokens;
using WatchIt.Website.Services.WebAPI.Accounts;
using WatchIt.Website.Services.WebAPI.Media;
using WatchIt.Website.Services.WebAPI.Photos;

namespace WatchIt.Website.Pages;

public partial class AuthPage
{
    #region SERVICES

    [Inject] public ILogger<AuthPage> Logger { get; set; } = default!;
    [Inject] public IAuthenticationService AuthenticationService { get; set; } = default!;
    [Inject] public ITokensService TokensService { get; set; } = default!;
    [Inject] public IMediaWebAPIService MediaWebAPIService { get; set; } = default!;
    [Inject] public IAccountsWebAPIService AccountsWebAPIService { get; set; } = default!;
    [Inject] public IPhotosWebAPIService PhotosWebAPIService { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;

    #endregion
    
    
    
    #region PARAMETERS

    [SupplyParameterFromQuery(Name = "redirect_to")]
    private string RedirectTo { get; set; } = "/";
    
    #endregion
    
    
    
    #region FIELDS
    
    private bool _loaded;

    private PhotoResponse? _background;

    private bool _isSingUp;
    
    

    private AuthenticateRequest _loginModel = new AuthenticateRequest
    {
        UsernameOrEmail = null,
        Password = null
    };

    private RegisterRequest _registerModel = new RegisterRequest
    {
        Username = null,
        Email = null,
        Password = null
    };
    private string _passwordConfirmation;

    private IEnumerable<string> _errors;

    #endregion

    
    
    #region METHODS

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            if (await AuthenticationService.GetAuthenticationStatusAsync())
            {
                NavigationManager.NavigateTo(WebUtility.UrlDecode(RedirectTo));
            }
            
            List<Task> endTasks = new List<Task>();
            
            // STEP 0
            endTasks.AddRange(
            [
                PhotosWebAPIService.GetPhotoRandomBackground(data => _background = data)
            ]);
            
            // END
            await Task.WhenAll(endTasks);
        
            _loaded = true;
            StateHasChanged();
        }
    }

    private async Task Login()
    {
        await AccountsWebAPIService.Authenticate(_loginModel, LoginSuccess, LoginBadRequest, LoginUnauthorized);
        
        async void LoginSuccess(AuthenticateResponse data)
        {
            await TokensService.SaveAuthenticationData(data);
            NavigationManager.NavigateTo(RedirectTo);
        }
        
        void LoginBadRequest(IDictionary<string, string[]> data)
        {
            _errors = data.SelectMany(x => x.Value).Select(x => $"• {x}");
        }
        
        void LoginUnauthorized()
        {
            _errors = [ "Incorrect account data" ];
        }
    }
    
    private async Task Register()
    {
        if (_registerModel.Password != _passwordConfirmation)
        {
            _errors = [ "Password fields don't match" ];
            return;
        }
        
        await AccountsWebAPIService.Register(_registerModel, RegisterSuccess, RegisterBadRequest);

        void RegisterSuccess(RegisterResponse data)
        {
            _isSingUp = false;
        }

        void RegisterBadRequest(IDictionary<string, string[]> data)
        {
            _errors = data.SelectMany(x => x.Value).Select(x => $"• {x}");
        }
    }

    #endregion
}