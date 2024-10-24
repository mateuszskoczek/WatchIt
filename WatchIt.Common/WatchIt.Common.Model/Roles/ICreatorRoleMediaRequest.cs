namespace WatchIt.Common.Model.Roles;

public interface ICreatorRoleMediaRequest : ICreatorRoleRequest
{
    #region PROPERTIES
    
    public long PersonId { get; set; }
    
    #endregion
}