using Microsoft.AspNetCore.Components;
using WatchIt.DTO.Models.Controllers.Media.Medium.Response;

namespace WatchIt.Website.Components.Panels.Pages.MediumPage;

public partial class MetadataPanel : Component
{
    #region PARAMETERS
    
    [Parameter] public required BaseMediumResponse Data { get; set; }
    
    #endregion 
}