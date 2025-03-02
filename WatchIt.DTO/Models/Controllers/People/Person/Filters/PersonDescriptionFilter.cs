using System.Text.RegularExpressions;
using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.People.Person.Filters;

public record PersonDescriptionFilter : Filter<Database.Model.People.Person>
{
    public PersonDescriptionFilter(string? descriptionRegex) : base(x =>
    (
        string.IsNullOrWhiteSpace(descriptionRegex)
        ||
        (
            !string.IsNullOrWhiteSpace(x.Description)
            &&
            Regex.IsMatch(x.Description, descriptionRegex, RegexOptions.IgnoreCase)
        )
    )) { }
}