using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model.Accounts;
using WatchIt.Common.Model.Genders;
using WatchIt.Website.Services.Client.Accounts;
using WatchIt.Website.Services.Client.Genders;

namespace WatchIt.Website.Components.Pages.UserEditPage.Panels;

public partial class ProfileEditFormPanelComponent : ComponentBase
{
    #region SERVICES

    [Inject] private NavigationManager NavigationManager { get; set; } = default!;
    [Inject] private IAccountsClientService AccountsClientService { get; set; } = default!;
    [Inject] private IGendersClientService GendersClientService { get; set; } = default!;
    
    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] public required AccountResponse AccountData { get; set; }
    [Parameter] public string Class { get; set; } = string.Empty;
    
    #endregion
    
    
    
    #region FIELDS

    private bool _loaded;
    private bool _saving;
    private string? _error;

    private IEnumerable<GenderResponse> _genders = [];
    
    private AccountProfileInfoRequest _accountProfileInfo = new AccountProfileInfoRequest();

    #endregion



    #region PRIVATE METHODS

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _accountProfileInfo = new AccountProfileInfoRequest(AccountData);
            await GendersClientService.GetAllGenders(successAction: data => _genders = data);
            
            _loaded = true;
            StateHasChanged();
        }
    }

    private async Task Save()
    {
        void Success()
        {
            _error = null;
            _saving = false;
        }
        
        void BadRequest(IDictionary<string, string[]> errors)
        {
            _error = errors.SelectMany(x => x.Value).FirstOrDefault() ?? "Unknown error";
            _saving = false;
        }

        void AuthError()
        {
            _error = "Authentication error";
            _saving = false;
        }

        _saving = true;
        await AccountsClientService.PutAccountProfileInfo(_accountProfileInfo, Success, BadRequest, AuthError);
    }

    #endregion
}