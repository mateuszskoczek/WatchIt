using System.Net;
using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model.Accounts;
using WatchIt.Common.Model.Media;
using WatchIt.Common.Model.Photos;
using WatchIt.Website.Services.Authentication;
using WatchIt.Website.Services.Tokens;
using WatchIt.Website.Services.Client.Accounts;
using WatchIt.Website.Services.Client.Media;
using WatchIt.Website.Services.Client.Photos;

namespace WatchIt.Website.Pages;

public partial class AuthPage
{
    #region SERVICES

    [Inject] public ILogger<AuthPage> Logger { get; set; } = default!;
    [Inject] public IAuthenticationService AuthenticationService { get; set; } = default!;
    [Inject] public ITokensService TokensService { get; set; } = default!;
    [Inject] public IMediaClientService MediaClientService { get; set; } = default!;
    [Inject] public IAccountsClientService AccountsClientService { get; set; } = default!;
    [Inject] public IPhotosClientService PhotosClientService { get; set; } = default!;
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
    private string? _formMessage;
    private bool _formMessageIsSuccess;
    
    private RegisterRequest _registerModel = new RegisterRequest
    {
        Username = null,
        Email = null,
        Password = null
    };
    private string _registerPasswordConfirmation;
    
    private AuthenticateRequest _loginModel = new AuthenticateRequest
    {
        UsernameOrEmail = null,
        Password = null
    };

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
                PhotosClientService.GetPhotoRandomBackground(data => _background = data)
            ]);
            
            // END
            await Task.WhenAll(endTasks);
        
            _loaded = true;
            StateHasChanged();
        }
    }

    private async Task Login()
    {
        void LoginBadRequest(IDictionary<string, string[]> data)
        {
            _formMessageIsSuccess = false;
            _formMessage = data.SelectMany(x => x.Value).FirstOrDefault();
        }

        void LoginUnauthorized()
        {
            _formMessageIsSuccess = false;
            _formMessage = "Incorrect account data";
        }
        
        async Task LoginSuccess(AuthenticateResponse data)
        {
            await TokensService.SaveAuthenticationData(data);
            NavigationManager.NavigateTo(RedirectTo);
        }
        
        
        await AccountsClientService.Authenticate(_loginModel, async (data) => await LoginSuccess(data), LoginBadRequest, LoginUnauthorized);
    }
    
    private async Task Register()
    {
        void RegisterSuccess(RegisterResponse data)
        {
            _formMessageIsSuccess = true;
            _formMessage = "You are registered. You can sign in now.";
            _isSingUp = false;
        }
        
        void RegisterBadRequest(IDictionary<string, string[]> data)
        {
            _formMessageIsSuccess = false;
            _formMessage = data.SelectMany(x => x.Value).FirstOrDefault();
        }
        
        
        if (_registerModel.Password != _registerPasswordConfirmation)
        {
            _formMessageIsSuccess = false;
            _formMessage = "Password fields don't match";
            return;
        }
        await AccountsClientService.Register(_registerModel, RegisterSuccess, RegisterBadRequest);
    }

    #endregion
}