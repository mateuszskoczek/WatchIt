using System.Text.RegularExpressions;
using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.People.Person.Filters;

public record PersonNameFilter : Filter<Database.Model.People.Person>
{
    public PersonNameFilter(string? nameRegex) : base(x =>
    (
        string.IsNullOrWhiteSpace(nameRegex)
        ||
        (
            !string.IsNullOrWhiteSpace(x.Name)
            &&
            Regex.IsMatch(x.Name, nameRegex, RegexOptions.IgnoreCase)
        )
    )) { }
}