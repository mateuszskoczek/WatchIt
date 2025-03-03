using System.Text.RegularExpressions;
using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Roles.RoleCreatorType.Filters;

public record RoleCreatorTypeNameFilter : Filter<Database.Model.Roles.RoleCreatorType>
{
    public RoleCreatorTypeNameFilter(string? nameRegex) : base(x => 
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