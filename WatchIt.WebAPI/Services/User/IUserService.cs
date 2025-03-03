using WatchIt.Database.Model.Accounts;

namespace WatchIt.WebAPI.Services.User;

public interface IUserService
{
    Task<Account> GetAccountAsync();
}