using Microsoft.AspNetCore.Components;

namespace WatchIt.Website.Components.Panels.Common;

public partial class ErrorPanel : Component
{
    #region PARAMETERS
    
    [Parameter] public string? ErrorMessage { get; set; }
    
    #endregion
}