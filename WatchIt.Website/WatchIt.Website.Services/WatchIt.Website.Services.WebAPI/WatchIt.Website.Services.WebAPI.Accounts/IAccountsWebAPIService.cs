using WatchIt.Common.Model.Accounts;

namespace WatchIt.Website.Services.WebAPI.Accounts;

public interface IAccountsWebAPIService
{
    Task Register(RegisterRequest data, Action<RegisterResponse> createdAction, Action<IDictionary<string, string[]>> badRequestAction);
    Task Authenticate(AuthenticateRequest data, Action<AuthenticateResponse> successAction, Action<IDictionary<string, string[]>> badRequestAction, Action unauthorizedAction);
    Task AuthenticateRefresh(Action<AuthenticateResponse> successAction, Action unauthorizedAction, Action forbiddenAction);
}