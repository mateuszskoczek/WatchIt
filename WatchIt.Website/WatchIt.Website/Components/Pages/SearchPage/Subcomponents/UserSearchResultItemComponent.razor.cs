using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model.Accounts;

namespace WatchIt.Website.Components.Pages.SearchPage.Subcomponents;

public partial class UserSearchResultItemComponent : ComponentBase
{
    #region PROPERTIES

    [Parameter] public required AccountResponse Item { get; set; }

    #endregion
}