using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model.Genres;
using WatchIt.Common.Model.Media;
using WatchIt.Common.Model.Movies;
using WatchIt.Website.Services.Utility.Authentication;
using WatchIt.Website.Services.WebAPI.Media;
using WatchIt.Website.Services.WebAPI.Movies;

namespace WatchIt.Website.Pages;

public partial class MediaDataPage : ComponentBase
{
    #region SERVICES

    [Inject] public NavigationManager NavigationManager { get; set; } = default!;
    [Inject] public IAuthenticationService AuthenticationService { get; set; } = default!;
    [Inject] public IMoviesWebAPIService MoviesWebAPIService { get; set; } = default!;
    [Inject] public IMediaWebAPIService MediaWebAPIService { get; set; } = default!;
    
    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] 
    public long Id { get; set; }
    
    #endregion



    #region FIELDS

    private bool _loaded = false;
    private string? _error;

    private MediaResponse? _media;
    private MovieResponse? _movie;
    private IEnumerable<GenreResponse> _genres;
    private MediaPhotoResponse? _background;
    private MediaPosterResponse? _poster;
    private User? _user;

    #endregion



    #region PRIVATE METHODS

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await MediaWebAPIService.Get(Id, data => _media = data, () => _error = $"Media with id {Id} was not found");

            if (_error is null)
            {
                Task backgroundTask = MediaWebAPIService.GetPhotoMediaRandomBackground(Id, data => _background = data);
                Task posterTask = MediaWebAPIService.GetPoster(Id, data => _poster = data);
                Task<User?> userTask = AuthenticationService.GetUserAsync();
                Task genresTask = MediaWebAPIService.GetGenres(Id, data => _genres = data);
                Task specificMediaTask;
                if (_media.Type == MediaType.Movie)
                {
                    specificMediaTask = MoviesWebAPIService.Get(Id, data => _movie = data);
                }
                else
                {
                    // TODO: download tv series info
                    specificMediaTask = null;
                }

                await Task.WhenAll(
                [
                    userTask,
                    specificMediaTask,
                    genresTask,
                    backgroundTask,
                    posterTask,
                ]);

                _user = await userTask;
            }
            
            _loaded = true;
            StateHasChanged();
        }
    }

    #endregion
}