using System.Diagnostics;
using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model.Movies;
using WatchIt.Website.Services.Utility.Authentication;
using WatchIt.Website.Services.WebAPI.Movies;

namespace WatchIt.Website.Pages;

public partial class MovieEditPage : ComponentBase
{
    #region SERVICES

    [Inject] public NavigationManager NavigationManager { get; set; } = default!;
    [Inject] public IAuthenticationService AuthenticationService { get; set; } = default!;
    [Inject] public IMoviesWebAPIService MoviesWebAPIService { get; set; } = default!;
    
    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] 
    public long? Id { get; set; }
    
    #endregion
    
    
    
    #region FIELDS

    private bool _loaded = false;
    private bool _authenticated = false;

    private MovieResponse? _movieInfo = null;

    private MovieRequest _movieData = new MovieRequest { Title = string.Empty };
    private IEnumerable<string>? _movieDataErrors = null;
    private string? _movieDataInfo = null;

    #endregion
    
    
    
    #region PRIVATE METHODS

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            User? user = await AuthenticationService.GetUserAsync();
            if (user is not null && user.IsAdmin)
            {
                _authenticated = true;

                await LoadData();
            }
            _loaded = true;
            StateHasChanged();
        }
    }

    private async Task LoadData()
    {
        if (Id is not null)
        {
            await MoviesWebAPIService.GetMovie(Id.Value, GetSuccessAction, NoIdAction);
        }

        return;

        void GetSuccessAction(MovieResponse data)
        {
            _movieInfo = data;
            _movieData = new MovieRequest(_movieInfo);
        }
        void NoIdAction() => NavigationManager.NavigateTo("/movies/new", true); // await for all
    }

    private async Task SaveData()
    {
        _movieDataErrors = null;
        _movieDataInfo = null;
        if (Id is null)
        {
            await MoviesWebAPIService.PostMovie(_movieData, PostSuccessAction, BadRequestAction, NoPermissionsAction, NoPermissionsAction);
        }
        else
        {
            await MoviesWebAPIService.PutMovie(Id.Value, _movieData, PutSuccessAction, BadRequestAction, NoPermissionsAction, NoPermissionsAction);
        }

        return;

        void PutSuccessAction() => _movieDataInfo = "Data saved";
        void PostSuccessAction(MovieResponse data) => NavigationManager.NavigateTo($"/movies/{data.Id}/edit", true);
        void BadRequestAction(IDictionary<string, string[]> errors) => _movieDataErrors = errors.SelectMany(x => x.Value);
        void NoPermissionsAction() => NavigationManager.Refresh(true);
    }

    #endregion
}