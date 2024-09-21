using System.Diagnostics;
using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model.Genres;
using WatchIt.Common.Model.Media;
using WatchIt.Common.Model.Movies;
using WatchIt.Website.Services.Utility.Authentication;
using WatchIt.Website.Services.WebAPI.Media;
using WatchIt.Website.Services.WebAPI.Movies;

namespace WatchIt.Website.Pages;

public partial class MediaPage : ComponentBase
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

    private bool _loaded;
    private string? _error;

    private MediaResponse? _media;
    
    private User? _user;
    
    private MediaPhotoResponse? _background;
    private MediaPosterResponse? _poster;
    private IEnumerable<GenreResponse> _genres;
    private MediaRatingResponse _globalRating;
    private MovieResponse? _movie;
    
    private short? _userRating;

    #endregion



    #region PRIVATE METHODS

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            List<Task> step1Tasks = new List<Task>();
            List<Task> step2Tasks = new List<Task>();
            List<Task> endTasks = new List<Task>();
            
            // STEP 0
            step1Tasks.AddRange(
            [
                MediaWebAPIService.GetMedia(Id, data => _media = data, () => _error = $"Media with id {Id} was not found")
            ]);
            
            // STEP 1
            await Task.WhenAll(step1Tasks);
            if (_error is null)
            {
                step2Tasks.AddRange(
                [
                    Task.Run(async () => _user = await AuthenticationService.GetUserAsync())
                ]);
                
                endTasks.AddRange(
                [
                    MediaWebAPIService.PostMediaView(Id),
                    MediaWebAPIService.GetPhotoMediaRandomBackground(Id, data => _background = data),
                    MediaWebAPIService.GetPoster(Id, data => _poster = data),
                    MediaWebAPIService.GetMediaGenres(Id, data => _genres = data),
                    MediaWebAPIService.GetMediaRating(Id, data => _globalRating = data),
                    _media.Type == MediaType.Movie ? MoviesWebAPIService.Get(Id, data => _movie = data) : Task.CompletedTask,
                ]);
            }
            
            // STEP 2
            await Task.WhenAll(step2Tasks);
            if (_error is null && _user is not null)
            {
                endTasks.AddRange(
                [
                    MediaWebAPIService.GetMediaRatingByUser(Id, _user.Id, data => _userRating = data)
                ]);
            }
            
            // END
            await Task.WhenAll(endTasks);
            
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