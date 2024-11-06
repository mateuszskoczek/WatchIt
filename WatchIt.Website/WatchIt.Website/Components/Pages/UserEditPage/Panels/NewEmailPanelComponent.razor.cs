using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model.Accounts;
using WatchIt.Website.Services.Client.Accounts;

namespace WatchIt.Website.Components.Pages.UserEditPage.Panels;

public partial class NewEmailPanelComponent : ComponentBase
{
    #region SERVICES
    
    [Inject] private IAccountsClientService AccountsClientService { get; set; } = default!;
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;
    
    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] public required AccountResponse AccountData { get; set; }
    
    #endregion
    
    
    
    #region FIELDS

    private AccountEmailRequest? _data;
    private string? _error;
    private bool _saving;
    private bool _saved;

    #endregion
    
    
    
    #region PRIVATE METHODS

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _data = new AccountEmailRequest
            {
                NewEmail = AccountData.Email,
            };
            StateHasChanged();
        }
    }

    private async Task Save()
    {
        void Success()
        {
            _saved = true;
            _saving = false;
            _data = new AccountEmailRequest
            {
                NewEmail = _data!.NewEmail
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
        await AccountsClientService.PatchAccountEmail(_data!, Success, BadRequest, Unauthorized);
    }

    #endregion
}