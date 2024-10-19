using Microsoft.AspNetCore.Components;

namespace WatchIt.Website.Components.Common.Panels;

public partial class ErrorPanelComponent : ComponentBase
{
    #region PARAMETERS
    
    [Parameter] public string? ErrorMessage { get; set; }
    
    #endregion
}