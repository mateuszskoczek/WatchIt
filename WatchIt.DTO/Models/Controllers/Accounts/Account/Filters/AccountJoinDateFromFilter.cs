using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Accounts.Account.Filters;

public record AccountJoinDateFromFilter : Filter<Database.Model.Accounts.Account>
{
    public AccountJoinDateFromFilter(DateOnly? query) : base(x => 
    (
        query == null
        ||
        x.JoinDate.UtcDateTime.CompareTo(query.Value) >= 0
    )) { }
}