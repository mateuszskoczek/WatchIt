namespace WatchIt.DTO.Models.Controllers.Roles.RoleCreatorType;

public class RoleCreatorTypeResponse : IRoleTypeResponse
{
    #region PROPERTIES
    
    public short Id { get; set; }
    public string Name { get; set; } = null!;

    #endregion
}