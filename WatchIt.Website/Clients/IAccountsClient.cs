using Refit;
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

namespace WatchIt.Website.Clients;

public interface IAccountsClient
{
    #region Main
    
    [Get("/")]
    Task<IApiResponse<IEnumerable<AccountResponse>>> GetAccounts([Query] AccountFilterQuery? filterQuery = null, [Query] OrderQuery? orderQuery = null, [Query] PagingQuery? pagingQuery = null, [Query][AliasAs("include_profile_pictures")] bool includeProfilePictures = false);
    
    [Get("/{id}")]
    Task<IApiResponse<AccountResponse>> GetAccount(long id, [Query][AliasAs("include_profile_pictures")] bool includeProfilePictures = false);

    [Post("/")]
    Task<IApiResponse<AccountResponse>> PostAccount([Body] AccountRequest body);
    
    #endregion
    
    #region Profile picture
    
    [Get("/{id}/profile_picture")]
    Task<IApiResponse<ImageResponse>> GetAccountProfilePicture(long id);

    [Put("/profile_picture")]
    Task<IApiResponse<ImageResponse>> PutAccountProfilePicture([Authorize]string token, [Body] ImageRequest body);

    [Delete("/profile_picture")]
    Task<IApiResponse> DeleteAccountProfilePicture([Authorize]string token);

    #endregion
    
    #region Background picture
    
    [Get("/{id}/background_picture")]
    Task<IApiResponse<PhotoResponse>> GetAccountBackgroundPicture(long id);

    [Put("/background_picture")]
    Task<IApiResponse<PhotoResponse>> PutAccountBackgroundPicture([Authorize]string token, [Body] AccountBackgroundPictureRequest body);

    [Delete("/background_picture")]
    Task<IApiResponse> DeleteAccountBackgroundPicture([Authorize]string token);

    #endregion
    
    #region Profile edit
    
    [Patch("/profile_info")]
    Task<IApiResponse> PatchAccountProfileInfo([Authorize]string token, [Body] AccountProfileInfoRequest body);
    
    [Patch("/username")]
    Task<IApiResponse> PatchAccountUsername([Authorize]string token, [Body] AccountUsernameRequest body);
    
    [Patch("/email")]
    Task<IApiResponse> PatchAccountEmail([Authorize]string token, [Body] AccountEmailRequest body);
    
    [Patch("/password")]
    Task<IApiResponse> PatchAccountPassword([Authorize]string token, [Body] AccountPasswordRequest body);
    
    #endregion

    #region Log out
    
    [Delete("/logout")]
    Task<IApiResponse> Logout([Body] AccountLogoutRequest body);

    [Delete("/logout_all")]
    Task<IApiResponse> LogoutAll([Authorize]string token);

    #endregion
    
    #region Follows
    
    [Get("/{id}/follows")]
    Task<IApiResponse<IEnumerable<AccountResponse>>> GetAccountFollows(long id, [Query] AccountFilterQuery? filterQuery = null, [Query] OrderQuery? orderQuery = null, [Query] PagingQuery? pagingQuery = null);
    
    [Get("/{id}/followers")]
    Task<IApiResponse<IEnumerable<AccountResponse>>> GetAccountFollowers(long id, [Query] AccountFilterQuery? filterQuery = null, [Query] OrderQuery? orderQuery = null, [Query] PagingQuery? pagingQuery = null);
    
    [Post("/follows/{followed_account_id}")]
    Task<IApiResponse> PostAccountFollow([Authorize]string token, [AliasAs("followed_account_id")] long followedAccountId);

    [Delete("/follows/{followed_account_id}")]
    Task<IApiResponse> DeleteAccountFollow([Authorize]string token, [AliasAs("followed_account_id")] long followedAccountId);

    #endregion
    
    #region Ratings
    
    [Get("/{id}/media")]
    Task<IApiResponse<IEnumerable<MediumUserRatedResponse>>> GetAccountRatedMedia(long id, [Query] MediumFilterQuery? filterQuery = null, [Query] MediumUserRatedFilterQuery<Medium>? userRatedFilterQuery = null, [Query] OrderQuery? orderQuery = null, [Query] PagingQuery? pagingQuery = null, [Query][AliasAs("include_pictures")] bool includePictures = false);
    
    [Get("/{id}/media/movies")]
    Task<IApiResponse<IEnumerable<MediumMovieUserRatedResponse>>> GetAccountRatedMediaMovies(long id, [Query] MediumMovieFilterQuery? filterQuery = null, [Query] MediumUserRatedFilterQuery<MediumMovie>? userRatedFilterQuery = null, [Query] OrderQuery? orderQuery = null, [Query] PagingQuery? pagingQuery = null, [Query][AliasAs("include_pictures")] bool includePictures = false);
    
    [Get("/{id}/media/series")]
    Task<IApiResponse<IEnumerable<MediumSeriesUserRatedResponse>>> GetAccountRatedMediaSeries(long id, [Query] MediumSeriesFilterQuery? filterQuery = null, [Query] MediumUserRatedFilterQuery<MediumSeries>? userRatedFilterQuery = null, [Query] OrderQuery? orderQuery = null, [Query] PagingQuery? pagingQuery = null, [Query][AliasAs("include_pictures")] bool includePictures = false);
    
    [Get("/{id}/people")]
    Task<IApiResponse<IEnumerable<PersonUserRatedResponse>>> GetAccountRatedPeople(long id, [Query] PersonFilterQuery? filterQuery = null, [Query] PersonUserRatedFilterQuery? userRatedFilterQuery = null, [Query] OrderQuery? orderQuery = null, [Query] PagingQuery? pagingQuery = null, [Query][AliasAs("include_pictures")] bool includePictures = false);
    
    #endregion
}
