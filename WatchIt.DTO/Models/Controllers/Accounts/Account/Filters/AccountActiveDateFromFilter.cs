using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Accounts.Account.Filters;

public record AccountActiveDateFromFilter : Filter<Database.Model.Accounts.Account>
{
    public AccountActiveDateFromFilter(DateOnly? query) : base(x => 
    (
        query == null
        ||
        x.ActiveDate.UtcDateTime.CompareTo(query.Value) >= 0
    )) { }
}