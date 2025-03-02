using Microsoft.AspNetCore.Components;
using Refit;
using WatchIt.DTO.Models.Generics.Image;

namespace WatchIt.Website.Components.Panels.Common;

public partial class HorizontalListPanel<TItem> : Component
{
    #region PARAMETERS

    [Parameter] public int Count { get; set; } = 5;
    [Parameter] public required string Title {get; set; }
    [Parameter] public required Func<Task<IApiResponse<IEnumerable<TItem>>>> GetItemsAction { get; set; }
    [Parameter] public required string ItemUrlFormatString { get; set; }
    [Parameter] public required Func<TItem, long> IdSource { get; set; }
    [Parameter] public required Func<TItem, string> NameSource { get; set; }
    [Parameter] public required string PosterPlaceholder { get; set; }
    [Parameter] public required Func<TItem, Task<ImageResponse?>> GetPictureAction { get; set; }
    [Parameter] public bool HidePlace { get; set; }
    [Parameter] public string? EmptyListMessage { get; set; }

    #endregion
    
    
    
    #region FIELDS

    private bool _loaded;
    
    private IEnumerable<TItem> _items = default!;
    
    #endregion
    
    
    
    #region PRIVATE METHODS
    
    protected override async Task OnFirstRenderAsync()
    {
        IApiResponse<IEnumerable<TItem>> response = await GetItemsAction();
        if (response.IsSuccessful)
        {
            _items = response.Content;
        }
        _loaded = true;
        StateHasChanged();
    }
    
    #endregion
}