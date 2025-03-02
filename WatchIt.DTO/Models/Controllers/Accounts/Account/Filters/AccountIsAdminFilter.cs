using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Accounts.Account.Filters;

public record AccountIsAdminFilter : Filter<Database.Model.Accounts.Account>
{
    public AccountIsAdminFilter(bool? query) : base(x => 
    (
        query == null
        ||
        x.IsAdmin == query
    )) { }
}