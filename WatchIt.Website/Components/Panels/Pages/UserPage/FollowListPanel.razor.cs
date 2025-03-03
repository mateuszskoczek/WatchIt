using Microsoft.AspNetCore.Components;
using WatchIt.DTO.Models.Controllers.Accounts.Account;

namespace WatchIt.Website.Components.Panels.Pages.UserPage;

public partial class FollowListPanel : Component
{
    #region PARAMETERS

    [Parameter] public required string Title { get; set; }
    [Parameter] public required Func<Task<IEnumerable<AccountResponse>>> GetItemsMethod { get; set; }

    #endregion
    
    
    
    #region FIELDS

    private bool _loaded;
    
    private IEnumerable<AccountResponse> _items = [];
    
    #endregion
    
    
    
    #region PRIVATE METHODS

    protected override async Task OnFirstRenderAsync()
    {
        _items = await GetItemsMethod();
            
        _loaded = true;
        StateHasChanged();
    }

    #endregion
}