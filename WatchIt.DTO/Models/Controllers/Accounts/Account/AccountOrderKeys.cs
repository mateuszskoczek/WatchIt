using System.Linq.Expressions;

namespace WatchIt.DTO.Models.Controllers.Accounts.Account;

public static class AccountOrderKeys
{
    public static readonly Dictionary<string, Expression<Func<Database.Model.Accounts.Account, object?>>> Base = new Dictionary<string, Expression<Func<Database.Model.Accounts.Account, object?>>>
    {
        { "id", x => x.Id },
        { "username", x => x.Username },
        { "email", x => x.Email },
        { "is_admin", x => x.IsAdmin },
        { "active_date", x => x.ActiveDate },
        { "join_date", x => x.JoinDate },
        { "description", x => x.Description },
        { "gender", x => x.Gender != null ? x.Gender.Name : null },
    };
}