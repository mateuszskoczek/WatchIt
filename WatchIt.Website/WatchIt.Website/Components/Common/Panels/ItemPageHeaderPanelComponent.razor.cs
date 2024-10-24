using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model;

namespace WatchIt.Website.Components.Common.Panels;

public partial class ItemPageHeaderPanelComponent : ComponentBase
{
    #region PARAMETERS
    
    [Parameter] public required string Name { get; set; }
    [Parameter] public string? Subname { get; set; }
    [Parameter] public string? Description { get; set; }
    
    [Parameter] public required string PosterPlaceholder { get; set; }
    [Parameter] public required Func<Action<Picture>, Task> GetPosterMethod { get; set; }

    #endregion
    
    
    
    #region FIELDS
    
    private Picture? _poster;
    
    #endregion



    #region PRIVATE METHODS

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            List<Task> endTasks = new List<Task>(1);
            
            // STEP 0
            endTasks.AddRange(
            [
                GetPosterMethod(data => _poster = data)
            ]);
            
            // END
            await Task.WhenAll(endTasks);
            
            StateHasChanged();
        }
    }

    #endregion
}