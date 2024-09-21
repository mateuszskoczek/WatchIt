using System.Diagnostics;
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
    private MediaRatingResponse _globalRating;
    private MediaPhotoResponse? _background;
    private MediaPosterResponse? _poster;
    private User? _user;
    
    private short? _userRating;

    #endregion



    #region PRIVATE METHODS

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await MediaWebAPIService.GetMedia(Id, data => _media = data, () => _error = $"Media with id {Id} was not found");

            if (_error is null)
            {
                Task backgroundTask = MediaWebAPIService.GetPhotoMediaRandomBackground(Id, data => _background = data);
                Task posterTask = MediaWebAPIService.GetPoster(Id, data => _poster = data);
                Task<User?> userTask = AuthenticationService.GetUserAsync();
                Task genresTask = MediaWebAPIService.GetMediaGenres(Id, data => _genres = data);
                Task globalRatingTask = MediaWebAPIService.GetMediaRating(Id, data => _globalRating = data);
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
                    globalRatingTask,
                    backgroundTask,
                    posterTask,
                ]);

                _user = await userTask;
            }

            if (_user is not null)
            {
                Task userRatingTask = MediaWebAPIService.GetMediaRatingByUser(Id, _user.Id, data => _userRating = data);
                
                await Task.WhenAll(
                [
                    userRatingTask,
                ]);
            }
            
            _loaded = true;
            StateHasChanged();
        }
    }

    private async Task AddRating(short rating)
    {
        if (_userRating == rating)
        {
            await MediaWebAPIService.DeleteMediaRating(Id);
            _userRating = null;
        }
        else
        {
            await MediaWebAPIService.PutMediaRating(Id, new MediaRatingRequest(rating));
            _userRating = rating;
        }
        await MediaWebAPIService.GetMediaRating(Id, data => _globalRating = data);
    }

    #endregion
}