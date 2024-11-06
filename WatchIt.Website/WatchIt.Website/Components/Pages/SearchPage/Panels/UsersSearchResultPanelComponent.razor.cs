using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model.Accounts;
using WatchIt.Website.Services.Client.Accounts;

namespace WatchIt.Website.Components.Pages.SearchPage.Panels;

public partial class UsersSearchResultPanelComponent : ComponentBase
{
    #region SERVICES
    
    [Inject] private IAccountsClientService AccountsClientService { get; set; } = default!;
    
    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] public required string Query { get; set; }
    
    #endregion
    
    
    
    #region FIELDS

    private bool _loaded;

    private AccountQueryParameters _query = new AccountQueryParameters
    {
        First = 5
    };
    
    private List<AccountResponse> _items = [];
    private bool _allItemsLoaded;
    private bool _itemsLoading;
    
    #endregion
    
    
    
    #region PRIVATE METHODS

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            // INIT
            _query.Username = Query;
            
            List<Task> endTasks = new List<Task>();
            
            // STEP 0
            endTasks.AddRange(
            [
                AccountsClientService.GetAccounts(_query, data =>
                {
                    _items.AddRange(data);
                    if (data.Count() < 5)
                    {
                        _allItemsLoaded = true;
                    }
                    else
                    {
                        _query.After = 5;
                    }
                })
            ]);
            
            // END
            await Task.WhenAll(endTasks);

            _loaded = true;
            StateHasChanged();
        }
    }

    private async Task DownloadItems()
    {
        _itemsLoading = true;
        await AccountsClientService.GetAccounts(_query, data =>
        {
            _items.AddRange(data);
            if (data.Count() < 5)
            {
                _allItemsLoaded = true;
            }
            else
            {
                _query.After += 5;
            }
            _itemsLoading = false;
        });
    }

    #endregion
}