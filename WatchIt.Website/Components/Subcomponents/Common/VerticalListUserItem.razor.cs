using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Components;
using WatchIt.DTO.Models.Controllers.Accounts.Account;

namespace WatchIt.Website.Components.Subcomponents.Common;

public partial class VerticalListUserItem : Component
{
    #region SERVICES

    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    #endregion
    
    
    
    #region PROPERTIES

    [Parameter] public required AccountResponse Item { get; set; }
    [Parameter] public bool ProfilePictureIncluded { get; set; }
    [Parameter] public int PictureSize { get; set; } = 90;

    #endregion
}