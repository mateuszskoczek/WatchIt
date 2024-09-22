using Microsoft.AspNetCore.Components;

namespace WatchIt.Website.Components;

public partial class ErrorComponent : ComponentBase
{
    #region PARAMETERS
    
    [Parameter] public string? ErrorMessage { get; set; }
    
    #endregion
}