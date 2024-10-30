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
    
    
    
    #region PRIVATE METHODS

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            List<Task> endTasks = new List<Task>();
            
            // STEP 0
            endTasks.AddRange(
            [
                AccountsClientService.GetAccountProfilePicture(Id, data => _picture = data)
            ]);
            
            // END
            await Task.WhenAll(endTasks);
            
            StateHasChanged();
        }
    }

    #endregion
}