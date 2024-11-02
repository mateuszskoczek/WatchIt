using Blazorise.Components.Autocomplete;
using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model;
using WatchIt.Common.Query;

namespace WatchIt.Website.Components.Common.Panels;

public partial class HorizontalListPanelComponent<TItem> : ComponentBase
{
    #region PARAMETERS

    [Parameter] public int Count { get; set; } = 5;
    [Parameter] public required string Title {get; set; }
    [Parameter] public required Func<Action<IEnumerable<TItem>>, Task> GetItemsAction { get; set; }
    [Parameter] public required string ItemUrlFormatString { get; set; }
    [Parameter] public required Func<TItem, long> IdSource { get; set; }
    [Parameter] public required Func<TItem, string> NameSource { get; set; }
    [Parameter] public required string PosterPlaceholder { get; set; }
    [Parameter] public required Func<long, Action<Picture>, Task> GetPictureAction { get; set; }

    #endregion
    
    
    
    #region FIELDS

    private bool _loaded;
    
    private IEnumerable<TItem> _items = default!;
    
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
                GetItemsAction(data => _items = data)
            ]);
            
            // END
            await Task.WhenAll(endTasks);
            
            _loaded = true;
            StateHasChanged();
        }
    }
    
    #endregion
}