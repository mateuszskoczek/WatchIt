using System.Net;
using Blazorise.Snackbar;
using Microsoft.AspNetCore.Components;
using Refit;
using WatchIt.DTO.Models.Controllers.Accounts.Account;
using WatchIt.DTO.Models.Controllers.Accounts.AccountUsername;
using WatchIt.Website.Clients;
using WatchIt.Website.Components.Layout;
using WatchIt.Website.Services.Authentication;

namespace WatchIt.Website.Components.Panels.Pages.UserEditPage;

public partial class NewUsernamePanel : Component
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

    private AccountUsernameRequest? _data;
    private bool _saving;

    #endregion
    
    
    
    #region PRIVATE METHODS

    protected override async Task OnFirstRenderAsync()
    {
        _data = new AccountUsernameRequest
        {
            Username = Data.Username,
        };
        StateHasChanged();
    }

    private async Task Save()
    {
        _saving = true;
        
        string token = await AuthenticationService.GetRawAccessTokenAsync() ?? string.Empty;
        
        IApiResponse response = await AccountsClient.PatchAccountUsername(token, _data!);
        switch (response)
        {
            case { IsSuccessful: true}:
                Data.Username = _data!.Username;
                _data.Password = string.Empty;
                await Base.SnackbarStack.PushAsync("Username successfully saved.", SnackbarColor.Success);
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