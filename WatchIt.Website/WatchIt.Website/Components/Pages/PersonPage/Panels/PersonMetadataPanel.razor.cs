using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model.Persons;

namespace WatchIt.Website.Components.Pages.PersonPage.Panels;

public partial class PersonMetadataPanel : ComponentBase
{
    #region PARAMETERS

    [Parameter] public required PersonResponse Item { get; set; }

    #endregion
}