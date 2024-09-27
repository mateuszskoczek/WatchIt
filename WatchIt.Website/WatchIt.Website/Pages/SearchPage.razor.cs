using System.Net;
using Microsoft.AspNetCore.Components;
using WatchIt.Website.Layout;
using WatchIt.Website.Services.WebAPI.Movies;
using WatchIt.Website.Services.WebAPI.Series;

namespace WatchIt.Website.Pages;

public partial class SearchPage : ComponentBase
{
    #region SERVICES

    [Inject] private IMoviesWebAPIService MoviesWebAPIService { get; set; } = default!;
    [Inject] private ISeriesWebAPIService SeriesWebAPIService { get; set; } = default!;
    
    #endregion


    
    #region FIELDS

    private bool _loaded;
    private string? _error;

    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] public string Query { get; set; }
    
    [CascadingParameter] public MainLayout Layout { get; set; }
    
    #endregion
    
    
    
    #region PROPERTIES
    
    public string DecodedQuery => WebUtility.UrlDecode(Query);
    
    #endregion



    #region PRIVATE METHODS

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _loaded = true;
            StateHasChanged();
        }
    }

    #endregion
}