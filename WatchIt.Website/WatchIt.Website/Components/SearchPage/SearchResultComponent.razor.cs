using Microsoft.AspNetCore.Components;
using WatchIt.Common.Query;

namespace WatchIt.Website.Components.SearchPage;

public partial class SearchResultComponent<TItem, TQuery> : ComponentBase where TQuery : QueryParameters
{
    #region PARAMETERS

    [Parameter] public required string Title { get; set; }
    [Parameter] public required TQuery Query { get; set; }
    [Parameter] public Func<TQuery, Action<IEnumerable<TItem>>, Task> DownloadingTask { get; set; }

    #endregion
    
    
    
    #region FIELDS

    private bool _loaded;
    
    private List<TItem> _items = [];
    private bool _allItemsLoaded;
    
    
    #endregion
    
    
    
    #region PRIVATE METHODS

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            // INIT
            Query.First = 5;
            
            List<Task> endTasks = new List<Task>();
            
            // STEP 0
            endTasks.AddRange(
            [
                DownloadingTask(Query, data => _items.AddRange(data))
            ]);
            
            // END
            await Task.WhenAll(endTasks);

            _loaded = true;
            StateHasChanged();
        }
    }

    #endregion
}