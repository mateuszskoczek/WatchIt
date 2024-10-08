namespace WatchIt.Common.Model.Roles;

public interface IActorRolePersonRequest : IActorRoleRequest
{
    #region PROPERTIES
    
    public long MediaId { get; set; }
    
    #endregion
}