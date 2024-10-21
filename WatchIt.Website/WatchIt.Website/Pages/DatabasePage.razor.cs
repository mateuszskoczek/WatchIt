using Microsoft.AspNetCore.Components;
using WatchIt.Website.Services.WebAPI.Media;
using WatchIt.Website.Services.WebAPI.Movies;
using WatchIt.Website.Services.WebAPI.Persons;
using WatchIt.Website.Services.WebAPI.Series;

namespace WatchIt.Website.Pages;

public partial class DatabasePage : ComponentBase
{
    #region SERVICES
    
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;
    [Inject] private IMediaWebAPIService MediaWebAPIService { get; set; } = default!;
    [Inject] private IMoviesWebAPIService MoviesWebAPIService { get; set; } = default!;
    [Inject] private ISeriesWebAPIService SeriesWebAPIService { get; set; } = default!;
    [Inject] private IPersonsWebAPIService PersonsWebAPIService { get; set; } = default!;
    
    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] public string? Type { get; set; }
    
    #endregion
    
    
    
    #region FIELDS

    private static IEnumerable<string> _databaseTypes = ["movies", "series", "people"];
    
    #endregion
    
    
    
    #region PRIVATE METHODS

    protected override void OnParametersSet()
    {
        if (!_databaseTypes.Contains(Type))
        {
            NavigationManager.NavigateTo($"/database/{_databaseTypes.First()}");
        }
    }

    #endregion
}