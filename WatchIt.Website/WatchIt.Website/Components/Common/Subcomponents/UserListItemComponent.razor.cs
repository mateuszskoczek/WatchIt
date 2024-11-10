using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model.Accounts;

namespace WatchIt.Website.Components.Common.Subcomponents;

public partial class UserListItemComponent : ComponentBase
{
    #region PROPERTIES

    [Parameter] public required AccountResponse Item { get; set; }
    [Parameter] public int PictureSize { get; set; } = 90;

    #endregion
}