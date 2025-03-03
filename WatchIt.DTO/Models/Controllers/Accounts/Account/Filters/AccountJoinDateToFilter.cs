using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Accounts.Account.Filters;

public record AccountJoinDateToFilter : Filter<Database.Model.Accounts.Account>
{
    public AccountJoinDateToFilter(DateOnly? query) : base(x => 
    (
        query == null
        ||
        x.JoinDate.UtcDateTime.CompareTo(query.Value) <= 0
    )) { }
}