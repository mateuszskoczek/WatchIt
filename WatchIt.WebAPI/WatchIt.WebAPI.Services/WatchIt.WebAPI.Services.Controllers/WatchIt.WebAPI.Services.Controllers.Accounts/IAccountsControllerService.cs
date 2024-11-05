using WatchIt.Common.Model.Accounts;
using WatchIt.Common.Model.Media;
using WatchIt.Common.Model.Movies;
using WatchIt.Common.Model.Persons;
using WatchIt.Common.Model.Series;
using WatchIt.WebAPI.Services.Controllers.Common;

namespace WatchIt.WebAPI.Services.Controllers.Accounts;

public interface IAccountsControllerService
{
    Task<RequestResult> Register(RegisterRequest data);
    Task<RequestResult> Authenticate(AuthenticateRequest data);
    Task<RequestResult> AuthenticateRefresh();
    Task<RequestResult> Logout();
    Task<RequestResult> GetAccountProfilePicture(long id);
    Task<RequestResult> PutAccountProfilePicture(AccountProfilePictureRequest data);
    Task<RequestResult> DeleteAccountProfilePicture();
    Task<RequestResult> GetAccountProfileBackground(long id);
    Task<RequestResult> PutAccountProfileBackground(AccountProfileBackgroundRequest data);
    Task<RequestResult> DeleteAccountProfileBackground();
    Task<RequestResult> GetAccountInfo(long id);
    Task<RequestResult> PutAccountProfileInfo(AccountProfileInfoRequest data);
    Task<RequestResult> GetAccountRatedMovies(long id, MovieRatedQueryParameters query);
    Task<RequestResult> GetAccountRatedSeries(long id, SeriesRatedQueryParameters query);
    Task<RequestResult> GetAccountRatedPersons(long id, PersonRatedQueryParameters query);
}