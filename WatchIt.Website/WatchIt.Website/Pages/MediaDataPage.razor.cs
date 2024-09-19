using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model.Media;
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
                Task<User?> userTask = AuthenticationService.GetUserAsync();
                Task posterTask = MediaWebAPIService.GetPoster(Id, data => _poster = data);

                await Task.WhenAll(
                [
                    userTask, 
                    posterTask
                ]);

                _user = await userTask;
            }
            
            _loaded = true;
            StateHasChanged();
        }
    }

    #endregion
}