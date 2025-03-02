using System.Text.RegularExpressions;
using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.People.Person.Filters;

public record PersonFullNameFilter : Filter<Database.Model.People.Person>
{
    public PersonFullNameFilter(string? fullNameRegex) : base(x =>
    (
        string.IsNullOrWhiteSpace(fullNameRegex)
        ||
        (
            !string.IsNullOrWhiteSpace(x.FullName)
            &&
            Regex.IsMatch(x.FullName, fullNameRegex, RegexOptions.IgnoreCase)
        )
    )) { }
}