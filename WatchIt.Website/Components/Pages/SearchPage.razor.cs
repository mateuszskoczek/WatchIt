using System.Net;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using WatchIt.Website.Clients;
using WatchIt.Website.Components.Layout;
using WatchIt.Website.Services.Authentication;

namespace WatchIt.Website.Components.Pages;

public partial class SearchPage : Page
{
    #region SERVICES

    [Inject] private IAuthenticationService AuthenticationService { get; set; } = null!;
    [Inject] private IMediaClient MediaClient { get; set; } = null!;
    [Inject] private IPeopleClient PeopleClient { get; set; } = null!;
    
    #endregion
    
    
    
    #region PARAMETERS
    
    [Parameter] public required string Query { get; set; }
    
    #endregion
    
    
    
    #region FIELDS
    
    private string _decodedQuery => WebUtility.UrlDecode(Query);

    #endregion
}