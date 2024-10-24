namespace WatchIt.Common.Model.Roles;

public interface ICreatorRolePersonRequest : ICreatorRoleRequest
{
    #region PROPERTIES
    
    public long MediaId { get; set; }
    
    #endregion
}