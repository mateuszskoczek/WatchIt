using Microsoft.AspNetCore.Components;

namespace WatchIt.Website.Components;

public partial class LoadingComponent : ComponentBase
{
    #region PARAMETERS

    [Parameter] public string Color { get; set; } = "dark";

    #endregion
}