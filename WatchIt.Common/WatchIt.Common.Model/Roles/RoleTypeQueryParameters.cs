using Microsoft.AspNetCore.Mvc;
using WatchIt.Common.Query;

namespace WatchIt.Common.Model.Roles;

public class RoleTypeQueryParameters : QueryParameters<RoleTypeResponse>
{
    #region PROPERTIES
    
    [FromQuery(Name = "name")]
    public string? Name { get; set; }
    
    #endregion
    
    
    
    #region PRIVATE METHODS

    protected override bool IsMeetingConditions(RoleTypeResponse item) => TestStringWithRegex(item.Name, Name);

    #endregion
}