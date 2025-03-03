using Microsoft.AspNetCore.Components;
using WatchIt.DTO.Models.Generics.Image;
using WatchIt.DTO.Models.Generics.Rating;
using WatchIt.DTO.Query;

namespace WatchIt.Website.Components.List;

public partial class List<TItem, TEntity, TQuery> : Component where TEntity : class where TQuery : IFilterQuery<TEntity>
{
    #region SERVICES
    
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    
    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] public required string Title { get; set; }
    
    [Parameter] public required Func<TQuery, OrderQuery, PagingQuery, Task<IEnumerable<TItem>>> GetItemsMethod { get; set; }
    
    [Parameter] public required Func<TItem, long> IdFunc { get; set; }
    [Parameter] public required Func<TItem, string> NameFunc { get; set; }
    [Parameter] public Func<TItem, string?> AdditionalNameInfoFunc { get; set; } = _ => null;
    [Parameter] public required Func<TItem, Task<ImageResponse?>> PictureFunc { get; set; }
    [Parameter] public required Func<TItem, RatingOverallResponse> GlobalRatingFunc { get; set; }
    
    [Parameter] public required string UrlIdTemplate { get; set; }
    [Parameter] public required string PicturePlaceholder { get; set; }
    
    [Parameter] public string? SecondaryRatingTitle { get; set; }
    
    [Parameter] public required Func<TItem, Task<RatingOverallResponse?>> GetGlobalRatingMethod { get; set; }
    [Parameter] public Func<TItem, Task<IRatingResponse?>>? GetSecondaryRatingMethod { get; set; }
    [Parameter] public Func<TItem, long, Task<IRatingUserResponse?>>? GetYourRatingMethod { get; set; }
    [Parameter] public Func<TItem, RatingRequest, Task>? PutYourRatingMethod { get; set; }
    [Parameter] public Func<TItem, Task>? DeleteYourRatingMethod { get; set; }
    
    [Parameter] public required RenderFragment ChildContent { get; set; }
    
    [Parameter] public TQuery Query { get; set; } = Activator.CreateInstance<TQuery>()!;
    [Parameter] public required Dictionary<string, string> SortingOptions { get; set; }

    #endregion
    
    
    
    #region FIELDS

    private OrderQuery _orderQuery = new OrderQuery();
    private PagingQuery _pagingQuery = new PagingQuery
    {
        First = 100,
    };

    private bool _loaded;
    private string? _error;

    private List<TItem> _items = new List<TItem>();
    private bool _allItemsLoaded;
    private bool _itemsLoading;
    
    #endregion



    #region PRIVATE METHODS

    protected override async Task OnFirstRenderAsync()
    {
        await base.OnFirstRenderAsync();

        _orderQuery.OrderBy = SortingOptions.Keys.First();
        await DownloadItems();
        
        _loaded = true;
        StateHasChanged();
    }
    
    private async Task DownloadItems()
    {
        _itemsLoading = true;
        IEnumerable<TItem> items = await GetItemsMethod(Query, _orderQuery, _pagingQuery);
        _items.AddRange(items);
        if (items.Count() < 100)
        {
            _allItemsLoaded = true;
        }
        else
        {
            _pagingQuery.After ??= 0;
            _pagingQuery.After += 100;
        }
        _itemsLoading = false;
    }

    private async Task UpdateItems()
    {
        _loaded = false;
        _pagingQuery.First = 100;
        _pagingQuery.After = null;
        _items.Clear();
        await DownloadItems();
        _loaded = true;
    }
    
    private async Task SortingAscendingChanged(ChangeEventArgs args)
    {
        _orderQuery.OrderAscending = (bool)args.Value!;
        await UpdateItems();
    }
    
    private async Task SortingOptionChanged(ChangeEventArgs args)
    {
        _orderQuery.OrderBy = args.Value!.ToString();
        await UpdateItems();
    }

    private async Task FilterApplied() => await UpdateItems();

    #endregion
}