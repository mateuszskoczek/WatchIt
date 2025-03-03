namespace WatchIt.DTO.Models.Controllers.Roles.Role.Request;

public class RoleActorRequest : RoleRequest
{
    #region PROPERTIES

    public short TypeId { get; set; }
    public string Name { get; set; } = null!;

    #endregion
}