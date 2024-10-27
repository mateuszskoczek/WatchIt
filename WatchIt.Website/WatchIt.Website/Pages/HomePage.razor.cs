using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model.Media;
using WatchIt.Common.Model.Movies;
using WatchIt.Common.Model.Series;
using WatchIt.Website.Layout;
using WatchIt.Website.Services.Client.Media;
using WatchIt.Website.Services.Client.Movies;
using WatchIt.Website.Services.Client.Persons;
using WatchIt.Website.Services.Client.Series;

namespace WatchIt.Website.Pages;

public partial class HomePage
{
    #region SERVICES

    [Inject] public NavigationManager NavigationManager { get; set; } = default!;
    [Inject] public IMediaClientService MediaClientService { get; set; } = default!;
    [Inject] public IMoviesClientService MoviesClientService { get; set; } = default!;
    [Inject] public ISeriesClientService SeriesClientService { get; set; } = default!;
    [Inject] public IPersonsClientService PersonsClientService { get; set; } = default!;
    
    #endregion
    
    
    
    #region PARAMETERS

    [CascadingParameter] public MainLayout Layout { get; set; } = default!;
    
    #endregion
    
    
    
    #region PRIVATE METHODS

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            Layout.BackgroundPhoto = null;
            
            StateHasChanged();
        }
    }

    #endregion
}