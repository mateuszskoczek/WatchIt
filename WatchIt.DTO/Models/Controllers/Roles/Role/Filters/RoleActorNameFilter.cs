using System.Text.RegularExpressions;
using WatchIt.Database.Model.Roles;
using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Roles.Role.Filters;

public record RoleActorNameFilter : Filter<RoleActor>
{
    public RoleActorNameFilter(string? regexQuery) : base(x =>
    (
        string.IsNullOrWhiteSpace(regexQuery)
        ||
        (
            !string.IsNullOrWhiteSpace(x.Name)
            &&
            Regex.IsMatch(x.Name, regexQuery, RegexOptions.IgnoreCase)
        )
    )) { }
}