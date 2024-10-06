using Microsoft.AspNetCore.Mvc;

namespace WatchIt.Common.Model.Roles;

public class CreatorRoleMediaQueryParameters : CreatorRoleQueryParameters
{
    #region PROPERTIES
    
    [FromQuery(Name = "person_id")]
    public long? PersonId { get; set; }
    
    #endregion



    #region PRIVATE METHODS

    protected override bool IsMeetingConditions(CreatorRoleResponse item) =>
    (
        base.IsMeetingConditions(item)
        &&
        Test(item.PersonId, PersonId)
    );

    #endregion
}