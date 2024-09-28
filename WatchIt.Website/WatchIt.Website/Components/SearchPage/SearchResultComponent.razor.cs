using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model;
using WatchIt.Common.Model.Rating;
using WatchIt.Common.Query;

namespace WatchIt.Website.Components.SearchPage;

public partial class SearchResultComponent<TItem, TQuery> : ComponentBase where TQuery : QueryParameters
{
    #region PARAMETERS

    [Parameter] public required string Title { get; set; }
    [Parameter] public required TQuery Query { get; set; }
    [Parameter] public required Func<TItem, long> IdSource { get; set; }
    [Parameter] public required Func<TItem, string> NameSource { get; set; }
    [Parameter] public Func<TItem, string?> AdditionalNameInfoSource { get; set; } = _ => null;
    [Parameter] public required string UrlIdTemplate { get; set; }
    [Parameter] public required Func<TQuery, Action<IEnumerable<TItem>>, Task> ItemDownloadingTask { get; set; }
    [Parameter] public required Func<long, Action<Picture>, Task> PictureDownloadingTask { get; set; }
    [Parameter] public required Func<long, Action<RatingResponse>, Task> RatingDownloadingTask { get; set; }

    #endregion
    
    
    
    #region FIELDS

    private bool _loaded;
    
    private List<TItem> _items = [];
    private bool _allItemsLoaded;
    private bool _itemsLoading;
    
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
                ItemDownloadingTask(Query, data =>
                {
                    _items.AddRange(data);
                    if (data.Count() < 5)
                    {
                        _allItemsLoaded = true;
                    }
                    else
                    {
                        Query.After = 5;
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
        await ItemDownloadingTask(Query, data =>
        {
            _items.AddRange(data);
            if (data.Count() < 5)
            {
                _allItemsLoaded = true;
            }
            else
            {
                Query.After += 5;
            }
            _itemsLoading = false;
        });
    }

    #endregion
}