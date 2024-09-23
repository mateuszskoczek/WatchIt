using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model.Media;
using WatchIt.Common.Model.Movies;
using WatchIt.Common.Model.Series;
using WatchIt.Website.Services.WebAPI.Media;
using WatchIt.Website.Services.WebAPI.Movies;
using WatchIt.Website.Services.WebAPI.Series;

namespace WatchIt.Website.Pages;

public partial class HomePage
{
    #region SERVICES

    [Inject] public NavigationManager NavigationManager { get; set; } = default!;
    [Inject] public IMediaWebAPIService MediaWebAPIService { get; set; } = default!;
    [Inject] public IMoviesWebAPIService MoviesWebAPIService { get; set; } = default!;
    [Inject] public ISeriesWebAPIService SeriesWebAPIService { get; set; } = default!;
    
    #endregion
    
    
    
    #region FIELDS
    
    private bool _loaded;
    private string? _error;
    
    private IDictionary<MovieResponse, MediaPosterResponse?> _topMovies = new Dictionary<MovieResponse, MediaPosterResponse?>();
    private IDictionary<SeriesResponse, MediaPosterResponse?> _topSeries = new Dictionary<SeriesResponse, MediaPosterResponse?>();
    
    #endregion
    
    
    
    #region PRIVATE METHODS

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            List<Task> step1Tasks = new List<Task>();
            List<Task> endTasks = new List<Task>();
            
            // STEP 0
            step1Tasks.AddRange(
            [
                MoviesWebAPIService.GetMoviesViewRank(successAction: data => _topMovies = data.ToDictionary(x => x, _ => default(MediaPosterResponse?))),
                SeriesWebAPIService.GetSeriesViewRank(successAction: data => _topSeries = data.ToDictionary(x => x, _ => default(MediaPosterResponse?))),
            ]);
            
            // STEP 1
            await Task.WhenAll(step1Tasks);
            endTasks.AddRange(
            [
                Parallel.ForEachAsync(_topMovies, async (x, _) => await MediaWebAPIService.GetPoster(x.Key.Id, y => _topMovies[x.Key] = y)),
                Parallel.ForEachAsync(_topSeries, async (x, _) => await MediaWebAPIService.GetPoster(x.Key.Id, y => _topSeries[x.Key] = y))
            ]);
            
            // END
            await Task.WhenAll(endTasks);
                
            _loaded = true;
            StateHasChanged();
        }
    }

    #endregion
}