using Microsoft.AspNetCore.Mvc;

namespace WatchIt.Common.Model.Roles;

public class ActorRolePersonQueryParameters : ActorRoleQueryParameters
{
    #region PROPERTIES
    
    [FromQuery(Name = "media_id")]
    public long? MediaId { get; set; }
    
    #endregion



    #region PRIVATE METHODS

    protected override bool IsMeetingConditions(ActorRoleResponse item) =>
    (
        base.IsMeetingConditions(item)
        &&
        Test(item.MediaId, MediaId)
    );

    #endregion
}