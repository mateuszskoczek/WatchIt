using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model;
using WatchIt.Common.Model.Movies;
using WatchIt.Common.Model.Rating;
using WatchIt.Common.Query;

namespace WatchIt.Website.Components.Common.ListComponent;

public partial class ListComponent<TItem, TQuery> : ComponentBase where TItem : IQueryOrderable<TItem> where TQuery : QueryParameters<TItem>
{
    #region SERVICES
    
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;
    
    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] public required string Title { get; set; }
    [Parameter] public required Func<TItem, long> IdSource { get; set; }
    [Parameter] public required Func<TItem, string> NameSource { get; set; }
    [Parameter] public Func<TItem, string?> AdditionalNameInfoSource { get; set; } = _ => null;
    [Parameter] public required Func<TItem, RatingResponse> RatingSource { get; set; }
    [Parameter] public Func<TItem, short?>? SecondaryRatingSource { get; set; }
    [Parameter] public string? SecondaryRatingTitle { get; set; }
    [Parameter] public required string UrlIdTemplate { get; set; }
    [Parameter] public required Func<long, Action<Picture>, Task> PictureDownloadingTask { get; set; }
    [Parameter] public required Func<TQuery, Action<IEnumerable<TItem>>, Task> ItemDownloadingTask { get; set; }
    [Parameter] public required Dictionary<string, string> SortingOptions { get; set; }
    [Parameter] public required RenderFragment ChildContent { get; set; }
    [Parameter] public required Func<long, Action<RatingResponse>, Task> GetGlobalRatingMethod { get; set; }
    [Parameter] public Func<long, long, Action<short>, Action, Task>? GetUserRatingMethod { get; set; }
    [Parameter] public Func<long, RatingRequest, Task>? PutRatingMethod { get; set; }
    [Parameter] public Func<long, Task>? DeleteRatingMethod { get; set; }
    [Parameter] public required string PosterPlaceholder { get; set; }

    #endregion
    
    
    
    #region FIELDS

    private bool _loaded;
    private string? _error;

    private List<TItem> _items = new List<TItem>();
    private bool _allItemsLoaded;
    private bool _itemsLoading;
    
    #endregion
    
    
    
    #region PROPERTIES
    
    public TQuery Query { get; set; } = Activator.CreateInstance<TQuery>()!;
    
    #endregion



    #region PRIVATE METHODS

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            // INIT
            Query.OrderBy = SortingOptions.Keys.First();
            Query.First = 100;

            List<Task> endTasks = new List<Task>();

            // STEP 0
            endTasks.AddRange(
            [
                ItemDownloadingTask(Query, data =>
                {
                    _items.AddRange(data);
                    if (data.Count() < 100)
                    {
                        _allItemsLoaded = true;
                    }
                    else
                    {
                        Query.After = 100;
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
        await ItemDownloadingTask(Query, AppendNewItems);
    }
    
    private async Task SortingAscendingChanged(ChangeEventArgs args)
    {
        Query.Order = (bool)args.Value! ? "asc" : "desc";
        await UpdateItems();
    }
    
    private async Task SortingOptionChanged(ChangeEventArgs args)
    {
        Query.OrderBy = args.Value!.ToString();
        await UpdateItems();
    }

    private async Task FilterApplied() => await UpdateItems();

    private async Task UpdateItems()
    {
        _loaded = false;
        Query.First = 100;
        Query.After = null;
        _items.Clear();
        await ItemDownloadingTask(Query, AppendNewItems);
        _loaded = true;
    }

    private void AppendNewItems(IEnumerable<TItem> items)
    {
        _items.AddRange(items);
        if (items.Count() < 100)
        {
            _allItemsLoaded = true;
        }
        else
        {
            Query.After += 100;
        }
        _itemsLoading = false;
    }

    #endregion
}