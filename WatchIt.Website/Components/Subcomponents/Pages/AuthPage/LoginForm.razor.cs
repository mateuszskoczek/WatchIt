using System.Net;
using Blazorise.Snackbar;
using Microsoft.AspNetCore.Components;
using Refit;
using WatchIt.DTO.Models.Controllers.Authentication;
using WatchIt.Website.Components.Layout;
using WatchIt.Website.Services.Authentication;

namespace WatchIt.Website.Components.Subcomponents.Pages.AuthPage;

public partial class LoginForm : Component
{
    #region SERVICES

    [Inject] public NavigationManager NavigationManager { get; set; } = null!;
    [Inject] public IAuthenticationService AuthenticationService { get; set; } = null!;

    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] public required string RedirectTo { get; set; }

    #endregion
    
    
    
    #region FIELDS

    private AuthenticationRequest _model = new AuthenticationRequest();

    #endregion



    #region PRIVATE METHODS

    private async Task Login()
    {
        IApiResponse response = await AuthenticationService.Login(_model);
        if (response.IsSuccessful)
        {
            NavigationManager.NavigateTo(RedirectTo, true);
        }
        else
        {
            string content = "Unknown error";
            switch (response.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    content = "Wrong username or password";
                    break;
                case HttpStatusCode.BadRequest:
                    if (response.Error is ValidationApiException ex)
                    {
                        string? exContent = ex.Content?.Errors.SelectMany(x => x.Value).FirstOrDefault();
                        if (exContent is not null)
                        {
                            content = exContent;
                        }
                    }
                    break;
            }
            await Base.SnackbarStack.PushAsync(content, SnackbarColor.Danger);
        }
    }

    #endregion
}