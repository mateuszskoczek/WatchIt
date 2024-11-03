using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model.Accounts;
using WatchIt.Website.Services.Client.Accounts;

namespace WatchIt.Website.Components.Common.Subcomponents;

public partial class AccountPictureComponent : ComponentBase
{
    #region SERVICES
    
    [Inject] private IAccountsClientService AccountsClientService { get; set; } = default!;
    
    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] public required long Id { get; set; }
    [Parameter] public required int Size { get; set; }

    [Parameter] public string Class { get; set; } = string.Empty;
    
    #endregion
    
    
    
    #region FIELDS
    
    private AccountProfilePictureResponse? _picture; 
    
    #endregion



    #region PUBLIC METHODS

    public async Task Reload()
    {
        await AccountsClientService.GetAccountProfilePicture(Id, data => _picture = data, notFoundAction: () => _picture = null);
        StateHasChanged();
    }

    #endregion
    
    
    
    #region PRIVATE METHODS

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await Reload();
        }
    }

    #endregion
}