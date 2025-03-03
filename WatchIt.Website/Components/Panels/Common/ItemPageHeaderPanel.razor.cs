using Microsoft.AspNetCore.Components;
using WatchIt.DTO.Models.Generics.Image;

namespace WatchIt.Website.Components.Panels.Common;

public partial class ItemPageHeaderPanel : Component
{
    #region PARAMETERS
    
    [Parameter] public required string Name { get; set; }
    [Parameter] public string? Subname { get; set; }
    [Parameter] public string? Description { get; set; }
    [Parameter] public ImageResponse? Poster { get; set; }
    
    [Parameter] public required string PosterPlaceholder { get; set; }

    #endregion
}