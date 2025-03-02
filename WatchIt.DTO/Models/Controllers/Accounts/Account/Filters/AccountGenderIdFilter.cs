using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Accounts.Account.Filters;

public record AccountGenderIdFilter : Filter<Database.Model.Accounts.Account>
{
    public AccountGenderIdFilter(short? genderId) : base(x => 
    (
        genderId == null
        ||
        x.GenderId == genderId
    )) { }
}