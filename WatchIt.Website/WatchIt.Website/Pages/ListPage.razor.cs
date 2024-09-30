using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model.Movies;
using WatchIt.Website.Services.WebAPI.Media;
using WatchIt.Website.Services.WebAPI.Movies;

namespace WatchIt.Website.Pages;

public partial class ListPage : ComponentBase
{
    #region SERVICES
    
    [Inject] private IMoviesWebAPIService MoviesWebAPIService { get; set; } = default!;
    [Inject] private IMediaWebAPIService MediaWebAPIService { get; set; } = default!;
    
    #endregion
    
    
    
    #region PARAMETERS
    
    
    
    #endregion
    
    
    
    #region FIELDS

    private bool _loaded;
    private string? _error;

    private MovieQueryParameters _query = new MovieQueryParameters { OrderBy = "rating.average" };
    private List<MovieResponse> _items = new List<MovieResponse>();
    private bool _allItemsLoaded;
    private bool _itemsLoading;
    
    #endregion
    
    
    
    #region PUBLIC METHODS

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            // INIT
            _query.First = 5;
            
            List<Task> endTasks = new List<Task>();
            
            // STEP 0
            endTasks.AddRange(
            [
                MoviesWebAPIService.GetAllMovies(_query, data =>
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
        await MoviesWebAPIService.GetAllMovies(_query, data =>
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