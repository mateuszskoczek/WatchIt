namespace WatchIt.Common.Model.Roles;

public interface IActorRoleRequest
{
    #region PROPERTIES
    
    public short TypeId { get; set; }
    
    public string Name { get; set; }
    
    #endregion
}