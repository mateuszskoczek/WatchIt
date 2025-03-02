using Microsoft.AspNetCore.Components;
using Refit;
using WatchIt.DTO.Models.Generics.Image;

namespace WatchIt.Website.Components.Subcomponents.Common;

public partial class HorizontalListItem : Component
{
    #region PARAMETERS
    
    [Parameter] public int? Place { get; set; }
    [Parameter] public required string Name { get; set; }
    [Parameter] public required string PosterPlaceholder { get; set; }
    [Parameter] public required Func<Task<ImageResponse?>> GetPosterAction { get; set; }
    
    #endregion
    
    
    
    #region FIELDS

    private ImageResponse? _poster;
    
    #endregion
    
    
    
    #region PRIVATE METHODS
    
    protected override async Task OnFirstRenderAsync()
    {
        _poster = await GetPosterAction();
        StateHasChanged();
    }
    
    #endregion
}