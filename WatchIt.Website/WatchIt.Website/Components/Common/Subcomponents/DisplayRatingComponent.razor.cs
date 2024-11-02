using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model.Rating;

namespace WatchIt.Website.Components.Common.Subcomponents;

public partial class DisplayRatingComponent : ComponentBase
{
    #region PARAMETERS
    
    [Parameter] public RatingResponse? Rating { get; set; }
    [Parameter] public short? SingleRating { get; set; }
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