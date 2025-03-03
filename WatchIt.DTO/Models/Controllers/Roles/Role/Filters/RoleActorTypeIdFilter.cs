using WatchIt.Database.Model.Roles;
using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Roles.Role.Filters;

public record RoleActorTypeIdFilter : Filter<RoleActor>
{
    public RoleActorTypeIdFilter(short? query) : base(x =>
    (
        query == null
        ||
        x.ActorTypeId == query
    )) { }
}