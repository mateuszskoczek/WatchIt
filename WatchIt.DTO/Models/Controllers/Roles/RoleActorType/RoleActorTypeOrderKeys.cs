using System.Linq.Expressions;

namespace WatchIt.DTO.Models.Controllers.Roles.RoleActorType;

public static class RoleActorTypeOrderKeys
{
    public static readonly Dictionary<string, Expression<Func<Database.Model.Roles.RoleActorType, object?>>> Base = new Dictionary<string, Expression<Func<Database.Model.Roles.RoleActorType, object?>>>
    {
        { "id", x => x.Id },
        { "name", x => x.Name },
    };
}