using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model;

namespace WatchIt.Website.Components.Common.Subcomponents;

public partial class HorizontalListItemComponent : ComponentBase
{
    #region PARAMETERS
    
    [Parameter] public int? Place { get; set; }
    [Parameter] public required string Name { get; set; }
    [Parameter] public required string PosterPlaceholder { get; set; }
    [Parameter] public required Func<Action<Picture>, Task> GetPosterAction { get; set; }
    
    #endregion
    
    
    
    #region FIELDS

    private Picture? _poster;
    
    #endregion
    
    
    
    #region PRIVATE METHODS
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            List<Task> endTasks = new List<Task>();
            
            // STEP 0
            endTasks.AddRange(
            [
                GetPosterAction(data => _poster = data),
            ]);
            
            // END
            await Task.WhenAll(endTasks);
            
            StateHasChanged();
        }
    }
    
    #endregion
}