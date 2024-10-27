using WatchIt.Common.Model.Accounts;

namespace WatchIt.Website.Services.Client.Accounts;

public interface IAccountsClientService
{
    Task Register(RegisterRequest data, Action<RegisterResponse>? createdAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null);
    Task Authenticate(AuthenticateRequest data, Action<AuthenticateResponse>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null);
    Task AuthenticateRefresh(Action<AuthenticateResponse>? successAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);
    Task Logout(Action? successAction = null);
    Task GetAccountProfilePicture(long id, Action<AccountProfilePictureResponse>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? notFoundAction = null);
    Task GetAccountInfoById(long id, Action<AccountResponse>? successAction = null, Action? notFoundAction = null);
    Task GetAccountInfo(Action<AccountResponse>? successAction = null, Action? unauthorizedAction = null, Action? notFoundAction = null);
    Task PutAccountInfo(AccountRequest data, Action<AccountResponse>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null, Action? notFoundAction = null);
}