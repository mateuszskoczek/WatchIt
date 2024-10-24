using Microsoft.AspNetCore.Mvc;
using WatchIt.Common.Query;

namespace WatchIt.Common.Model.Roles;

public abstract class ActorRoleQueryParameters : QueryParameters<ActorRoleResponse>
{
    #region PROPERTIES
    
    [FromQuery(Name = "type_id")]
    public short? TypeId { get; set; }
    
    [FromQuery(Name = "name")]
    public string? Name { get; set; }
    
    #endregion
    
    
    
    #region PRIVATE METHODS
    
    protected override bool IsMeetingConditions(ActorRoleResponse item) =>
    (
        Test(item.TypeId, TypeId)
        &&
        TestStringWithRegex(item.Name, Name)
    );

    #endregion

    
}