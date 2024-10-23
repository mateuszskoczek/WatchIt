using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model.Rating;

namespace WatchIt.Website.Components.Common.Subcomponents;

public partial class TitledDisplayRatingComponent : ComponentBase
{
    #region PARAMETERS
    
    [Parameter] public required RatingResponse Rating { get; set; }
    [Parameter] public required string Title { get; set; }
    
    #endregion
}