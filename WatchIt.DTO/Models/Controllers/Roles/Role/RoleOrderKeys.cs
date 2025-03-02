using System.Linq.Expressions;
using WatchIt.Database.Model.Roles;

namespace WatchIt.DTO.Models.Controllers.Roles.Role;

public class RoleOrderKeys
{
    public static Dictionary<string, Expression<Func<T, object?>>> Base<T>() where T : Database.Model.Roles.Role => new Dictionary<string, Expression<Func<T, object?>>>
    {
        { "person", item => item.PersonId },
        { "medium", item => item.MediumId },
        { "medium.release_date", item => item.Medium.ReleaseDate }
    };
    
    public static readonly Dictionary<string, Expression<Func<RoleActor, object?>>> RoleActor = new Dictionary<string, Expression<Func<RoleActor, object?>>>
    {
        { "type_id", x => x.ActorTypeId },
        { "name", x => x.Name },
    };
    
    public static readonly Dictionary<string, Expression<Func<RoleCreator, object?>>> RoleCreator = new Dictionary<string, Expression<Func<RoleCreator, object?>>>
    {
        { "type_id", x => x.CreatorTypeId },
    };
}