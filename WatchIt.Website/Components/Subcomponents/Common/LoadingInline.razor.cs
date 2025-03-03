using Microsoft.AspNetCore.Components;

namespace WatchIt.Website.Components.Subcomponents.Common;

public partial class LoadingInline : Component
{
    #region PARAMETERS

    [Parameter] public string Content { get; set; } = "Loading...";

    #endregion
}