using Microsoft.AspNetCore.Components;
using WatchIt.DTO.Models.Generics.Rating;

namespace WatchIt.Website.Components.Subcomponents.Common;

public partial class DisplayRating : Component
{
    #region PARAMETERS
    
    [Parameter] public required IRatingResponse? Rating { get; set; }
    [Parameter] public DisplayRatingComponentEmptyMode EmptyMode { get; set; } = DisplayRatingComponentEmptyMode.NoRatings;
    [Parameter] public double Scale { get; set; } = 1;

    #endregion
    
    
    
    #region ENUMS

    public enum DisplayRatingComponentEmptyMode
    {
        NoRatings,
        DoubleDash,
    }
    
    #endregion
}