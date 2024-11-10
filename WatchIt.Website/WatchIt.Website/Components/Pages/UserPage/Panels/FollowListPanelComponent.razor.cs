using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model.Accounts;

namespace WatchIt.Website.Components.Pages.UserPage.Panels;

public partial class FollowListPanelComponent : ComponentBase
{
    #region PARAMETERS

    [Parameter] public required string Title { get; set; }
    [Parameter] public required Func<Action<IEnumerable<AccountResponse>>, Task> DownloadItemsMethod { get; set; }

    #endregion
    
    
    
    #region FIELDS

    private bool _loaded;
    
    private List<AccountResponse> _items = [];
    
    #endregion
    
    
    
    #region PRIVATE METHODS

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await DownloadItemsMethod.Invoke(data => _items.AddRange(data));
            
            _loaded = true;
            StateHasChanged();
        }
    }

    #endregion
}