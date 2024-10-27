using Microsoft.AspNetCore.Components;
using WatchIt.Website.Layout;
using WatchIt.Website.Services.Client.Media;
using WatchIt.Website.Services.Client.Movies;
using WatchIt.Website.Services.Client.Persons;
using WatchIt.Website.Services.Client.Series;

namespace WatchIt.Website.Pages;

public partial class DatabasePage : ComponentBase
{
    #region SERVICES
    
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;
    [Inject] private IMediaClientService MediaClientService { get; set; } = default!;
    [Inject] private IMoviesClientService MoviesClientService { get; set; } = default!;
    [Inject] private ISeriesClientService SeriesClientService { get; set; } = default!;
    [Inject] private IPersonsClientService PersonsClientService { get; set; } = default!;
    
    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] public string? Type { get; set; }
    
    [CascadingParameter] public MainLayout Layout { get; set; }
    
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

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            // INIT
            Layout.BackgroundPhoto = null;
            
            StateHasChanged();
        }
    }

    #endregion
}