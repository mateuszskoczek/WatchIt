using Microsoft.AspNetCore.Components;

namespace WatchIt.Website.Components;

public partial class LoadingButtonContentComponent : ComponentBase
{
    #region PARAMETERS
    
    [Parameter] public bool IsLoading { get; set; }
    [Parameter] public string? LoadingContent { get; set; }
    [Parameter] public string Content { get; set; }
    [Parameter] public RenderFragment ChildContent { get; set; }
    
    #endregion
}