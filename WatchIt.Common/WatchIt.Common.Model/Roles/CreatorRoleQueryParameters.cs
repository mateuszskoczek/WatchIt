using Microsoft.AspNetCore.Mvc;
using WatchIt.Common.Query;

namespace WatchIt.Common.Model.Roles;

public abstract class CreatorRoleQueryParameters : QueryParameters<CreatorRoleResponse>
{
    #region PROPERTIES
    
    [FromQuery(Name = "type_id")]
    public short? TypeId { get; set; }
    
    #endregion
    
    
    
    #region PRIVATE METHODS
    
    protected override bool IsMeetingConditions(CreatorRoleResponse item) =>
    (
        Test(item.TypeId, TypeId)
    );

    #endregion
}