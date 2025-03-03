using System.Text.RegularExpressions;
using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Accounts.Account.Filters;

public record AccountDescriptionFilter : Filter<Database.Model.Accounts.Account>
{
    public AccountDescriptionFilter(string? descriptionRegex) : base(x => 
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