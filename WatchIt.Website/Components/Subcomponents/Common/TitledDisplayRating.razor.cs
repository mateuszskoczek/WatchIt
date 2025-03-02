using Microsoft.AspNetCore.Components;
using WatchIt.DTO.Models.Generics.Rating;

namespace WatchIt.Website.Components.Subcomponents.Common;

public partial class TitledDisplayRating : Component
{
    #region PARAMETERS
    
    [Parameter] public required IRatingResponse? Rating { get; set; }
    [Parameter] public required string Title { get; set; }
    
    #endregion
}