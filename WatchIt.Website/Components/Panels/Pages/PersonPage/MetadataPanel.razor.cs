using Microsoft.AspNetCore.Components;
using WatchIt.DTO.Models.Controllers.People.Person;

namespace WatchIt.Website.Components.Panels.Pages.PersonPage;

public partial class MetadataPanel : Component
{
    #region PARAMETERS

    [Parameter] public required PersonResponse Data { get; set; }

    #endregion
}