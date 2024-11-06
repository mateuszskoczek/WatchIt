using System.Diagnostics;
using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model.Genres;
using WatchIt.Common.Model.Media;
using WatchIt.Common.Model.Movies;
using WatchIt.Common.Model.Photos;
using WatchIt.Common.Model.Rating;
using WatchIt.Common.Model.Series;
using WatchIt.Website.Layout;
using WatchIt.Website.Services.Authentication;
using WatchIt.Website.Services.Client.Media;
using WatchIt.Website.Services.Client.Movies;
using WatchIt.Website.Services.Client.Series;

namespace WatchIt.Website.Pages;

public partial class MediaPage : ComponentBase
{
    #region SERVICES

    [Inject] public NavigationManager NavigationManager { get; set; } = default!;
    [Inject] public IAuthenticationService AuthenticationService { get; set; } = default!;
    [Inject] public IMediaClientService MediaClientService { get; set; } = default!;
    [Inject] public IMoviesClientService MoviesClientService { get; set; } = default!;
    [Inject] public ISeriesClientService SeriesClientService { get; set; } = default!;
    
    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] public long Id { get; set; }
    
    [CascadingParameter] public MainLayout Layout { get; set; }
    
    #endregion



    #region FIELDS

    private bool _loaded;
    private string? _error;

    private MediaResponse? _media;
    
    private User? _user;
    
    private MediaPosterResponse? _poster;
    private IEnumerable<GenreResponse> _genres;
    private RatingResponse _globalRating;
    private MovieResponse? _movie;
    private SeriesResponse? _series;
    
    private short? _userRating;

    #endregion



    #region PRIVATE METHODS

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            Layout.BackgroundPhoto = null;
            
            List<Task> step1Tasks = new List<Task>();
            List<Task> step2Tasks = new List<Task>();
            List<Task> endTasks = new List<Task>();
            
            // STEP 0
            step1Tasks.AddRange(
            [
                MediaClientService.GetMedia(Id, data => _media = data, () => _error = $"Media with id {Id} was not found")
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
                    MediaClientService.PostMediaView(Id),
                    MediaClientService.GetMediaPhotoRandomBackground(Id, data => Layout.BackgroundPhoto = data),
                    MediaClientService.GetMediaGenres(Id, data => _genres = data),
                    MediaClientService.GetMediaRating(Id, data => _globalRating = data),
                    _media.Type == MediaType.Movie ? MoviesClientService.GetMovie(Id, data => _movie = data) : SeriesClientService.GetSeries(Id, data => _series = data),
                ]);
            }
            
            // STEP 2
            await Task.WhenAll(step2Tasks);
            if (_error is null && _user is not null)
            {
                endTasks.AddRange(
                [
                    MediaClientService.GetMediaRatingByUser(Id, _user.Id, data => _userRating = data)
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
            await MediaClientService.DeleteMediaRating(Id);
            _userRating = null;
        }
        else
        {
            await MediaClientService.PutMediaRating(Id, new RatingRequest(rating));
            _userRating = rating;
        }
        await MediaClientService.GetMediaRating(Id, data => _globalRating = data);
    }

    #endregion
}