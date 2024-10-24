namespace WatchIt.Common.Model.Roles;

public interface IActorRoleMediaRequest : IActorRoleRequest
{
    #region PROPERTIES
    
    public long PersonId { get; set; }
    
    #endregion
}