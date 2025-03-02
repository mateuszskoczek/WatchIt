using System.Text.RegularExpressions;
using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Roles.RoleActorType.Filters;

public record RoleActorTypeNameFilter : Filter<Database.Model.Roles.RoleActorType>
{
    public RoleActorTypeNameFilter(string? nameRegex) : base(x => 
    (
        string.IsNullOrWhiteSpace(nameRegex)
        ||
        (
            !string.IsNullOrWhiteSpace(x.Name)
            &&
            Regex.IsMatch(x.Name, nameRegex, RegexOptions.IgnoreCase)
        )
    )) { }
};