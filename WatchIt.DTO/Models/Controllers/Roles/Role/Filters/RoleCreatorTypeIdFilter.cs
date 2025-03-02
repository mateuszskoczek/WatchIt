using WatchIt.Database.Model.Roles;
using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Roles.Role.Filters;

public record RoleCreatorTypeIdFilter : Filter<RoleCreator>
{
    public RoleCreatorTypeIdFilter(short? query) : base(x =>
    (
        query == null
        ||
        x.CreatorTypeId == query
    )) { }
}