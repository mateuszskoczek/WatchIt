using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model.Accounts;
using WatchIt.Website.Services.Authentication;
using WatchIt.Website.Services.Client.Accounts;

namespace WatchIt.Website.Components.Pages.UserEditPage.Panels;

public partial class NewUsernamePanelComponent : ComponentBase
{
    #region SERVICES
    
    [Inject] private IAuthenticationService AuthenticationService { get; set; } = default!;
    [Inject] private IAccountsClientService AccountsClientService { get; set; } = default!;
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;
    
    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] public required long Id { get; set; }
    
    #endregion
    
    
    
    #region FIELDS

    private AccountUsernameRequest? _data;
    private string? _error;
    private bool _saving;
    private bool _saved;

    #endregion
    
    
    
    #region PRIVATE METHODS

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            User? user = await AuthenticationService.GetUserAsync();

            if (user is null)
            {
                return;
            }
            
            await AccountsClientService.GetAccountInfo(user.Id, data =>
            {
                _data = new AccountUsernameRequest
                {
                    NewUsername = data.Username
                };
                StateHasChanged();
            });
        }
    }

    private async Task Save()
    {
        void Success()
        {
            _saved = true;
            _saving = false;
            _data = new AccountUsernameRequest
            {
                NewUsername = _data!.NewUsername
            };
            NavigationManager.Refresh(true);
        }

        void BadRequest(IDictionary<string, string[]> errors)
        {
            _error = errors.SelectMany(x => x.Value).FirstOrDefault() ?? "Unknown error";
            _saving = false;
        }

        void Unauthorized()
        {
            _error = "Incorrect password";
            _saving = false;
        }
        
        _saving = true;
        _saved = false;
        _error = null;
        await AccountsClientService.PatchAccountUsername(_data!, Success, BadRequest, Unauthorized);
    }

    #endregion
}