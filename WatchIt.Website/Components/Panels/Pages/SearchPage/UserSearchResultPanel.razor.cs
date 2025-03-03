using Blazorise.Snackbar;
using Microsoft.AspNetCore.Components;
using Refit;
using WatchIt.DTO.Models.Controllers.Accounts.Account;
using WatchIt.DTO.Query;
using WatchIt.Website.Clients;
using WatchIt.Website.Components.Layout;

namespace WatchIt.Website.Components.Panels.Pages.SearchPage;

public partial class UserSearchResultPanel : Component
{
    #region SERVICES
    
    [Inject] private IAccountsClient AccountsClient { get; set; } = null!;
    
    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] public required string Query { get; set; }
    
    #endregion
    
    
    
    #region FIELDS

    
    private readonly AccountFilterQuery _filterQuery = new AccountFilterQuery();
    private readonly PagingQuery _pagingQuery = new PagingQuery
    {
        First = 5
    };
    private List<AccountResponse> _items = [];
    
    private bool _loaded;
    private bool _allItemsLoaded;
    private bool _itemsLoading;
    
    #endregion
    
    
    
    #region PRIVATE METHODS

    protected override async Task OnFirstRenderAsync()
    {
        await DownloadItems();
        _pagingQuery.After = 5;
        
        _loaded = true;
        StateHasChanged();
    }
    
    private async Task DownloadItems()
    {
        _itemsLoading = true;
        
        IApiResponse<IEnumerable<AccountResponse>> response = await AccountsClient.GetAccounts(_filterQuery, pagingQuery: _pagingQuery, includeProfilePictures: true);
        if (!response.IsSuccessful)
        {
            await Base.SnackbarStack.PushAsync("An error has occured. Users could not be loaded", SnackbarColor.Danger);
        }
        IEnumerable<AccountResponse> items = response.Content ?? [];
        
        _items.AddRange(items);
        if (items.Count() < 5)
        {
            _allItemsLoaded = true;
        }
        else
        {
            _pagingQuery.After ??= 0;
            _pagingQuery.After += 5;
        }
        _itemsLoading = false;
    }

    #endregion
}