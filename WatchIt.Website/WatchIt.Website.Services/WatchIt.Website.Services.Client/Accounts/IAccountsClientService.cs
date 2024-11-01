using WatchIt.Common.Model.Accounts;
using WatchIt.Common.Model.Movies;
using WatchIt.Common.Model.Series;

namespace WatchIt.Website.Services.Client.Accounts;

public interface IAccountsClientService
{
    Task Register(RegisterRequest data, Action<RegisterResponse>? createdAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null);
    Task Authenticate(AuthenticateRequest data, Action<AuthenticateResponse>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null);
    Task AuthenticateRefresh(Action<AuthenticateResponse>? successAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);
    Task Logout(Action? successAction = null);
    Task GetAccountProfilePicture(long id, Action<AccountProfilePictureResponse>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? notFoundAction = null);
    Task GetAccountInfo(long id, Action<AccountResponse>? successAction = null, Action? notFoundAction = null);
    Task PutAccountInfo(AccountRequest data, Action<AccountResponse>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null, Action? notFoundAction = null);
    Task GetAccountRatedMovies(long id, MovieRatedQueryParameters query, Action<IEnumerable<MovieRatedResponse>>? successAction = null, Action? notFoundAction = null);
    Task GetAccountRatedSeries(long id, SeriesRatedQueryParameters query, Action<IEnumerable<SeriesRatedResponse>>? successAction = null, Action? notFoundAction = null);
}