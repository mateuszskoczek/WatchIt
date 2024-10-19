using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model;
using WatchIt.Common.Model.Rating;

namespace WatchIt.Website.Components.Common.Subcomponents;

public partial class ListItemComponent : ComponentBase
{
    #region PARAMETERS
    
    [Parameter] public required long Id { get; set; }
    [Parameter] public required string Name { get; set; }
    [Parameter] public string? AdditionalNameInfo { get; set; }
    [Parameter] public required RatingResponse Rating { get; set; }
    [Parameter] public required Func<long, Action<Picture>, Task> PictureDownloadingTask { get; set; }
    [Parameter] public int PictureHeight { get; set; } = 150;

    #endregion
    
    
    
    #region FIELDS

    private bool _loaded;
    
    private Picture? _picture;
    
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
                PictureDownloadingTask(Id, picture => _picture = picture),
            ]);
            
            await Task.WhenAll(endTasks);
            
            _loaded = true;
            StateHasChanged();
        }
    }

    #endregion
}