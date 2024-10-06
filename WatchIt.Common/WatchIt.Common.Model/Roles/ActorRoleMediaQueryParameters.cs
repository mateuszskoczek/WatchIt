using Microsoft.AspNetCore.Mvc;

namespace WatchIt.Common.Model.Roles;

public class ActorRoleMediaQueryParameters : ActorRoleQueryParameters
{
    #region PROPERTIES
    
    [FromQuery(Name = "person_id")]
    public long? PersonId { get; set; }
    
    #endregion



    #region PRIVATE METHODS

    protected override bool IsMeetingConditions(ActorRoleResponse item) =>
    (
        base.IsMeetingConditions(item)
        &&
        Test(item.PersonId, PersonId)
    );

    #endregion
}