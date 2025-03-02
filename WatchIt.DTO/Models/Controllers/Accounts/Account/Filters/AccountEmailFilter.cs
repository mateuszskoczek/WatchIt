using System.Text.RegularExpressions;
using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Accounts.Account.Filters;

public record AccountEmailFilter : Filter<Database.Model.Accounts.Account>
{
    public AccountEmailFilter(string? emailRegex) : base(x => 
    (
        string.IsNullOrWhiteSpace(emailRegex)
        ||
        (
            !string.IsNullOrWhiteSpace(x.Username)
            &&
            Regex.IsMatch(x.Username, emailRegex, RegexOptions.IgnoreCase)
        )
    )) { }
}