namespace WatchIt.DTO.Models.Controllers.Roles.Role.Response;

public abstract class RoleResponse
{
    #region PROPERTIES
    
    public Guid Id { get; set; }
    public long MediumId { get; set; }
    public long PersonId { get; set; }
    public short TypeId { get; set; }
    
    #endregion
}