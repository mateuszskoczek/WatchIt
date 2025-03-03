namespace WatchIt.Database.Model.Roles;

public class RoleActorType
{
    #region PROPERTIES
    
    public short Id { get; set; }
    public string Name { get; set; } = default!;
    public uint Version { get; set; }
    
    #endregion
    
    
    
    #region NAVIGATION
    
    public virtual IEnumerable<RoleActor> Roles { get; set; } = new List<RoleActor>();
    
    #endregion
}