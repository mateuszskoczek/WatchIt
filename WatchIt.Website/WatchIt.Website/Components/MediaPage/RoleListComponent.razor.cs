using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model.Roles;

namespace WatchIt.Website.Components.MediaPage;

public partial class RoleListComponent<TRole> : ComponentBase where TRole : IRoleResponse
{
    #region PROPERTIES
    
    [Parameter] public required IEnumerable<TRole> Role { get; set; }
    
    #endregion
}