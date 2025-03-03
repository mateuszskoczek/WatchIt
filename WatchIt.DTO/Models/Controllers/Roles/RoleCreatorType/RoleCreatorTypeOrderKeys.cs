using System.Linq.Expressions;

namespace WatchIt.DTO.Models.Controllers.Roles.RoleCreatorType;

public static class RoleCreatorTypeOrderKeys
{
    public static readonly Dictionary<string, Expression<Func<Database.Model.Roles.RoleCreatorType, object?>>> Base = new Dictionary<string, Expression<Func<Database.Model.Roles.RoleCreatorType, object?>>>
    {
        { "id", x => x.Id },
        { "name", x => x.Name },
    };
}