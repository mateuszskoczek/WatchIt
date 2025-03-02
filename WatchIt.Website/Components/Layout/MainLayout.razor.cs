using Blazorise.Snackbar;
using Microsoft.AspNetCore.Components;
using Refit;
using WatchIt.DTO.Models.Controllers.Genres.Genre;
using WatchIt.Website.Clients;
using WatchIt.Website.Services.Authentication;

namespace WatchIt.Website.Components.Layout;

public partial class MainLayout : LayoutComponentBase
{
    #region SERVICES

    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private IAuthenticationService AuthenticationService { get; set; } = null!;
    [Inject] private IGenresClient GenresClient { get; set; } = null!;

    #endregion



    #region FIELDS

    private bool _searchbarVisible;

    private IEnumerable<GenreResponse> _genres = [];

    #endregion
    
    
    
    #region PARAMETERS
    
    [CascadingParameter] public required BaseLayout BaseLayout { get; set; }
    
    #endregion
    
    
    
    #region PRIVATE METHODS
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await OnFirstRenderAsync();
        }
        await base.OnAfterRenderAsync(firstRender);
    }

    protected async Task OnFirstRenderAsync()
    {
        IApiResponse<IEnumerable<GenreResponse>> genresResponse = await GenresClient.GetGenres();
        switch (genresResponse.IsSuccessful)
        {
            case true: _genres = genresResponse.Content; break;
            case false: await BaseLayout.SnackbarStack.PushAsync("An error occured. Genres could not be loaded", SnackbarColor.Danger); break;
        }
    }

    private async Task Logout()
    {
        await AuthenticationService.Logout();
        NavigationManager.Refresh(true);
    }

    #endregion
}