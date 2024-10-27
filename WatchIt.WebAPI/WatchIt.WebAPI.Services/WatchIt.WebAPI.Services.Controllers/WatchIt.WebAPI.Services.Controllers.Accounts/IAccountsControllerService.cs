using WatchIt.Common.Model.Accounts;
using WatchIt.WebAPI.Services.Controllers.Common;

namespace WatchIt.WebAPI.Services.Controllers.Accounts;

public interface IAccountsControllerService
{
    Task<RequestResult> Register(RegisterRequest data);
    Task<RequestResult> Authenticate(AuthenticateRequest data);
    Task<RequestResult> AuthenticateRefresh();
    Task<RequestResult> Logout();
    Task<RequestResult> GetAccountProfilePicture(long id);
    Task<RequestResult> GetAccountInfo();
    Task<RequestResult> GetAccountInfo(long id);
    Task<RequestResult> PutAccountInfo(AccountRequest data);
}