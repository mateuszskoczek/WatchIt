using Microsoft.AspNetCore.Components;
using WatchIt.Website.Components.Layout;
using WatchIt.Website.Services.Authentication;

namespace WatchIt.Website.Components.Pages;

public partial class AdminPage : Page
{
    #region PARAMETERS
    
    [CascadingParameter] public required BaseLayout Layout { get; set; }
    
    #endregion
}