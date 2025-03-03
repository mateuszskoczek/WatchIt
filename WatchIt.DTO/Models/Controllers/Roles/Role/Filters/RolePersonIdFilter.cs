using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Roles.Role.Filters;

public record RolePersonIdFilter<T> : Filter<T> where T : Database.Model.Roles.Role
{
    public RolePersonIdFilter(long? query) : base(x =>
    (
        query == null
        ||
        x.PersonId == query
    )) { }
}