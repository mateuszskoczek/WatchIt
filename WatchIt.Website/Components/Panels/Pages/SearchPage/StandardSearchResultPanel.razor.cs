using Microsoft.AspNetCore.Components;
using WatchIt.DTO.Models.Generics.Image;
using WatchIt.DTO.Models.Generics.Rating;
using WatchIt.DTO.Query;

namespace WatchIt.Website.Components.Panels.Pages.SearchPage;

public partial class StandardSearchResultPanel<TItem, TQuery> : Component where TQuery : IFilterQuery
{
    #region PARAMETERS

    [Parameter] public required string Title { get; set; }
    [Parameter] public required TQuery Query { get; set; }
    
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

    #endregion
    
    
    
    #region FIELDS

    private static readonly OrderQuery _orderQuery = new OrderQuery
    {
        OrderBy = "rating.average"
    };
    private readonly PagingQuery _pagingQuery = new PagingQuery
    {
        First = 5
    };
    private readonly List<TItem> _items = [];
    
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
        IEnumerable<TItem> items = await GetItemsMethod(Query, _orderQuery, _pagingQuery);
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