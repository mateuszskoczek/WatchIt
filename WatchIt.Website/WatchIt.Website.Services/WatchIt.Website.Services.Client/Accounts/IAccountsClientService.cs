using WatchIt.Common.Model.Accounts;
using WatchIt.Common.Model.Movies;
using WatchIt.Common.Model.Persons;
using WatchIt.Common.Model.Photos;
using WatchIt.Common.Model.Series;

namespace WatchIt.Website.Services.Client.Accounts;

public interface IAccountsClientService
{
    Task Register(RegisterRequest data, Action<RegisterResponse>? createdAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null);
    Task Authenticate(AuthenticateRequest data, Action<AuthenticateResponse>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null);
    Task AuthenticateRefresh(Action<AuthenticateResponse>? successAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);
    Task Logout(Action? successAction = null);
    Task GetAccountProfilePicture(long id, Action<AccountProfilePictureResponse>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? notFoundAction = null);
    Task PutAccountProfilePicture(AccountProfilePictureRequest data, Action<AccountProfilePictureResponse>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null);
    Task DeleteAccountProfilePicture(Action? successAction = null, Action? unauthorizedAction = null);
    Task GetAccountProfileBackground(long id, Action<PhotoResponse>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? notFoundAction = null);
    Task PutAccountProfileBackground(AccountProfileBackgroundRequest data, Action<PhotoResponse>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null);
    Task DeleteAccountProfileBackground(Action? successAction = null, Action? unauthorizedAction = null);
    Task GetAccounts(AccountQueryParameters? query = null, Action<IEnumerable<AccountResponse>>? successAction = null);
    Task GetAccount(long id, Action<AccountResponse>? successAction = null, Action? notFoundAction = null);
    Task PutAccountProfileInfo(AccountProfileInfoRequest data, Action? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null);
    Task PatchAccountUsername(AccountUsernameRequest data, Action? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null);
    Task PatchAccountEmail(AccountEmailRequest data, Action? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null);
    Task PatchAccountPassword(AccountPasswordRequest data, Action? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null);
    Task GetAccountRatedMovies(long id, MovieRatedQueryParameters? query = null, Action<IEnumerable<MovieRatedResponse>>? successAction = null, Action? notFoundAction = null);
    Task GetAccountRatedSeries(long id, SeriesRatedQueryParameters? query = null, Action<IEnumerable<SeriesRatedResponse>>? successAction = null, Action? notFoundAction = null);
    Task GetAccountRatedPersons(long id, PersonRatedQueryParameters? query = null, Action<IEnumerable<PersonRatedResponse>>? successAction = null, Action? notFoundAction = null);
    Task GetAccountFollows(long id, AccountQueryParameters? query = null, Action<IEnumerable<AccountResponse>>? successAction = null, Action? notFoundAction = null);
    Task GetAccountFollowers(long id, AccountQueryParameters? query = null, Action<IEnumerable<AccountResponse>>? successAction = null, Action? notFoundAction = null);
    Task PostAccountFollow(long userId, Action? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null);
    Task DeleteAccountFollow(long userId, Action? successAction = null, Action? unauthorizedAction = null);
}