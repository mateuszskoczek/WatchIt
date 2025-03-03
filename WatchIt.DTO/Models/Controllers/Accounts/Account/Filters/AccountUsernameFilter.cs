using System.Text.RegularExpressions;
using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Accounts.Account.Filters;

public record AccountUsernameFilter : Filter<Database.Model.Accounts.Account>
{
    public AccountUsernameFilter(string? usernameRegex) : base(x => 
    (
        string.IsNullOrWhiteSpace(usernameRegex)
        ||
        (
            !string.IsNullOrWhiteSpace(x.Username)
            &&
            Regex.IsMatch(x.Username, usernameRegex, RegexOptions.IgnoreCase)
        )
    )) { }
}