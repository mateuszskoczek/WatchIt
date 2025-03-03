using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Accounts.Account.Filters;

public record AccountActiveDateToFilter : Filter<Database.Model.Accounts.Account>
{
    public AccountActiveDateToFilter(DateOnly? query) : base(x => 
    (
        query == null
        ||
        x.ActiveDate.UtcDateTime.CompareTo(query.Value) <= 0
    )) { }
}