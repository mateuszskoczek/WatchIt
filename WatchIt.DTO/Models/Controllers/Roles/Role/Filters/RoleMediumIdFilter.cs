using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Roles.Role.Filters;

public record RoleMediumIdFilter<T> : Filter<T> where T : Database.Model.Roles.Role
{
    public RoleMediumIdFilter(long? query) : base(x =>
    (
        query == null
        ||
        x.MediumId == query
    )) { }
}