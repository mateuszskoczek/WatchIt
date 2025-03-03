using Ardalis.Result;
using WatchIt.Database.Model.Media;
using WatchIt.DTO.Models.Controllers.Accounts.Account;
using WatchIt.DTO.Models.Controllers.Accounts.AccountBackgroundPicture;
using WatchIt.DTO.Models.Controllers.Accounts.AccountEmail;
using WatchIt.DTO.Models.Controllers.Accounts.AccountLogout;
using WatchIt.DTO.Models.Controllers.Accounts.AccountPassword;
using WatchIt.DTO.Models.Controllers.Accounts.AccountProfileInfo;
using WatchIt.DTO.Models.Controllers.Accounts.AccountUsername;
using WatchIt.DTO.Models.Controllers.Media.Medium.Query;
using WatchIt.DTO.Models.Controllers.Media.Medium.Response;
using WatchIt.DTO.Models.Controllers.People.Person;
using WatchIt.DTO.Models.Controllers.People.Person.Query;
using WatchIt.DTO.Models.Controllers.Photos.Photo;
using WatchIt.DTO.Models.Generics.Image;
using WatchIt.DTO.Query;

namespace WatchIt.WebAPI.BusinessLogic.Accounts;

public interface IAccountsBusinessLogic
{
    #region Main
    
    Task<Result<IEnumerable<AccountResponse>>> GetAccounts(AccountFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, bool includeProfilePictures);
    Task<Result<AccountResponse>> GetAccount(long accountId, bool includeProfilePictures);
    Task<Result<AccountResponse>> PostAccount(AccountRequest body);
    
    #endregion
    
    #region Profile picture
    
    Task<Result<ImageResponse>> GetAccountProfilePicture(long accountId);
    Task<Result<ImageResponse>> PutAccountProfilePicture(ImageRequest body);
    Task<Result> DeleteAccountProfilePicture();
    
    #endregion

    #region Background picture

    Task<Result<PhotoResponse>> GetAccountBackgroundPicture(long accountId);
    Task<Result<PhotoResponse>> PutAccountBackgroundPicture(AccountBackgroundPictureRequest body);
    Task<Result> DeleteAccountBackgroundPicture();

    #endregion
    
    #region Profile edit
    
    Task<Result> PatchAccountProfileInfo(AccountProfileInfoRequest body);
    Task<Result> PatchAccountUsername(AccountUsernameRequest body);
    Task<Result> PatchAccountEmail(AccountEmailRequest body);
    Task<Result> PatchAccountPassword(AccountPasswordRequest body);
    
    #endregion

    #region Log out
    
    Task<Result> Logout(AccountLogoutRequest body);
    Task<Result> LogoutAll();

    #endregion
    
    #region Follows
    
    Task<Result<IEnumerable<AccountResponse>>> GetAccountFollows(long accountId, AccountFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery);
    Task<Result<IEnumerable<AccountResponse>>> GetAccountFollowers(long accountId, AccountFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery);
    Task<Result> PostAccountFollow(long followedAccountId);
    Task<Result> DeleteAccountFollow(long followedAccountId);
    
    #endregion
    
    #region Media

    Task<Result<IEnumerable<MediumUserRatedResponse>>> GetAccountRatedMedia(long accountId, MediumFilterQuery filterQuery, MediumUserRatedFilterQuery<Medium> userRatedFilterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, bool includePictures);
    Task<Result<IEnumerable<MediumMovieUserRatedResponse>>> GetAccountRatedMediaMovies(long accountId, MediumMovieFilterQuery filterQuery, MediumUserRatedFilterQuery<MediumMovie> userRatedFilterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, bool includePictures);
    Task<Result<IEnumerable<MediumSeriesUserRatedResponse>>> GetAccountRatedMediaSeries(long accountId, MediumSeriesFilterQuery filterQuery, MediumUserRatedFilterQuery<MediumSeries> userRatedFilterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, bool includePictures);
    Task<Result<IEnumerable<PersonUserRatedResponse>>> GetAccountRatedPeople(long accountId, PersonFilterQuery filterQuery, PersonUserRatedFilterQuery userRatedFilterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, bool includePictures);

    #endregion
}