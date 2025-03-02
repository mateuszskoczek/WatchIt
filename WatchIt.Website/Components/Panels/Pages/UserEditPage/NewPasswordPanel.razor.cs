using System.Net;
using Blazorise.Snackbar;
using Microsoft.AspNetCore.Components;
using Refit;
using WatchIt.DTO.Models.Controllers.Accounts.Account;
using WatchIt.DTO.Models.Controllers.Accounts.AccountPassword;
using WatchIt.Website.Clients;
using WatchIt.Website.Components.Layout;
using WatchIt.Website.Services.Authentication;

namespace WatchIt.Website.Components.Panels.Pages.UserEditPage;

public partial class NewPasswordPanel : Component
{
    #region SERVICES
    
    [Inject] private IAuthenticationService AuthenticationService { get; set; } = null!;
    [Inject] private IAccountsClient AccountsClient { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    
    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] public required AccountResponse Data { get; set; }
    
    #endregion
    
    
    
    #region FIELDS

    private AccountPasswordRequest? _data = new AccountPasswordRequest();
    private bool _saving;

    #endregion
    
    
    
    #region PRIVATE METHODS

    private async Task Save()
    {
        _saving = true;
        
        string token = await AuthenticationService.GetRawAccessTokenAsync() ?? string.Empty;
        
        IApiResponse response = await AccountsClient.PatchAccountPassword(token, _data!);
        switch (response)
        {
            case { IsSuccessful: true}:
                _data = new AccountPasswordRequest();
                await Base.SnackbarStack.PushAsync("Password successfully saved.", SnackbarColor.Success);
                break;
            case { StatusCode: HttpStatusCode.Forbidden } or { StatusCode: HttpStatusCode.Unauthorized }:
                await Base.SnackbarStack.PushAsync("Incorrect password", SnackbarColor.Danger);
                break;
            case { StatusCode: HttpStatusCode.BadRequest }:
                string? content = "An unknown error occured.";
                if (response.Error is ValidationApiException ex)
                {
                    string? exContent = ex.Content?.Errors.SelectMany(x => x.Value).FirstOrDefault();
                    if (exContent is not null)
                    {
                        content = exContent;
                    }
                }
                await Base.SnackbarStack.PushAsync(content, SnackbarColor.Danger);
                break;
            default:
                await Base.SnackbarStack.PushAsync("An unknown error occured.", SnackbarColor.Danger);
                break;
        }
        
        _saving = false;
    }

    #endregion
}