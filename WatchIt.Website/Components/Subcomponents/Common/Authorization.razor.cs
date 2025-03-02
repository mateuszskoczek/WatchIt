using Microsoft.AspNetCore.Components;
using WatchIt.Website.Components.Layout;

namespace WatchIt.Website.Components.Subcomponents.Common;

public partial class Authorization : Component
{
    #region PARAMETERS

    [CascadingParameter] public required BaseLayout BaseLayout { get; set; }
    
    [Parameter] public RenderFragment? NotAuthorized { get; set; }
    [Parameter] public RenderFragment? Authorized { get; set; }
    [Parameter] public RenderFragment? Loading { get; set; }
    [Parameter] public bool Admin { get; set; }

    #endregion
}