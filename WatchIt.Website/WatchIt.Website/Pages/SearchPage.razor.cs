using System.Net;
using Microsoft.AspNetCore.Components;
using WatchIt.Website.Layout;
using WatchIt.Website.Services.Client.Media;
using WatchIt.Website.Services.Client.Movies;
using WatchIt.Website.Services.Client.Persons;
using WatchIt.Website.Services.Client.Series;

namespace WatchIt.Website.Pages;

public partial class SearchPage : ComponentBase
{
    #region SERVICES

    [Inject] private IMoviesClientService MoviesClientService { get; set; } = default!;
    [Inject] private ISeriesClientService SeriesClientService { get; set; } = default!;
    [Inject] private IMediaClientService MediaClientService { get; set; } = default!;
    [Inject] private IPersonsClientService PersonsClientService { get; set; } = default!;
    
    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] public string Query { get; set; }
    
    [CascadingParameter] public MainLayout Layout { get; set; }
    
    #endregion
    
    
    
    #region PROPERTIES
    
    public string DecodedQuery => WebUtility.UrlDecode(Query);
    
    #endregion
}