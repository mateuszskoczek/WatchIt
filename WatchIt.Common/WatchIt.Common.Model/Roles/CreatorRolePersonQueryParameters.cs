using Microsoft.AspNetCore.Mvc;

namespace WatchIt.Common.Model.Roles;

public class CreatorRolePersonQueryParameters : CreatorRoleQueryParameters
{
    #region PROPERTIES
    
    [FromQuery(Name = "media_id")]
    public long? MediaId { get; set; }
    
    #endregion



    #region PRIVATE METHODS

    protected override bool IsMeetingConditions(CreatorRoleResponse item) =>
    (
        base.IsMeetingConditions(item)
        &&
        Test(item.MediaId, MediaId)
    );

    #endregion
}