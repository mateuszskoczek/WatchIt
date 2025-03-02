using System.Net;
using Blazorise.Snackbar;
using Microsoft.AspNetCore.Components;
using Refit;
using WatchIt.DTO.Models.Controllers.Accounts.Account;
using WatchIt.Website.Clients;
using WatchIt.Website.Components.Layout;

namespace WatchIt.Website.Components.Subcomponents.Pages.AuthPage;

public partial class RegisterForm : Component
{
    #region SERVICES

    [Inject] public IAccountsClient AccountsClient { get; set; } = null!;

    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] public Action? RegisteredSuccessfully { get; set; }

    #endregion
    
    
    
    #region FIELDS

    private AccountRequest _model = new AccountRequest();

    #endregion



    #region PRIVATE METHODS

    private async Task Register()
    {
        IApiResponse<AccountResponse> response = await AccountsClient.PostAccount(_model);
        if (response.IsSuccessful)
        {
            RegisteredSuccessfully?.Invoke();
            await Base.SnackbarStack.PushAsync("You have been registered successfully. You can log in now.", SnackbarColor.Success);
        }
        else
        {
            string content = "Unknown error";
            if (response.Error is ValidationApiException ex)
            {
                string? exContent = ex.Content?.Errors.SelectMany(x => x.Value).FirstOrDefault();
                if (exContent is not null)
                {
                    content = exContent;
                }
            }
            await Base.SnackbarStack.PushAsync(content, SnackbarColor.Danger);
        }
    }

    #endregion
}