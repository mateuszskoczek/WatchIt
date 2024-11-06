using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model.Accounts;
using WatchIt.Website.Services.Client.Accounts;

namespace WatchIt.Website.Components.Pages.UserEditPage.Panels;

public partial class NewPasswordPanelComponent : ComponentBase
{
    #region SERVICES
    
    [Inject] private IAccountsClientService AccountsClientService { get; set; } = default!;
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;
    
    #endregion
    
    
    
    #region FIELDS

    private AccountPasswordRequest _data = new AccountPasswordRequest();
    private string? _error;
    private bool _saving;
    private bool _saved;

    #endregion
    
    
    
    #region PRIVATE METHODS

    private async Task Save()
    {
        void Success()
        {
            _saved = true;
            _saving = false;
            _data = new AccountPasswordRequest();
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
        await AccountsClientService.PatchAccountPassword(_data, Success, BadRequest, Unauthorized);
    }

    #endregion
}