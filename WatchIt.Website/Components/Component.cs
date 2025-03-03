using Microsoft.AspNetCore.Components;
using WatchIt.Website.Components.Layout;

namespace WatchIt.Website.Components;

public abstract class Component : ComponentBase
{
    #region PARAMETERS

    [CascadingParameter] public required BaseLayout Base { get; set; }

    #endregion



    #region PRIVATE METHODS

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        if (firstRender)
        {
            await OnFirstRenderAsync();
        }
    }

    protected virtual async Task OnFirstRenderAsync()
    {
        await Task.CompletedTask;
    }

    #endregion
}