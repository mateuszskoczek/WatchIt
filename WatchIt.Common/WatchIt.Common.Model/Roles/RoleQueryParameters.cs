using Microsoft.AspNetCore.Mvc;
using WatchIt.Common.Query;

namespace WatchIt.Common.Model.Roles;

public class RoleQueryParameters : QueryParameters<RoleResponse>
{
    #region PROPERTIES
    
    [FromQuery(Name = "name")]
    public string? Name { get; set; }
    
    #endregion
    
    
    
    #region PRIVATE METHODS

    protected override bool IsMeetingConditions(RoleResponse item) => TestStringWithRegex(item.Name, Name);

    #endregion
}