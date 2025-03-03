using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
using WatchIt.WebAPI.BusinessLogic.Accounts;

namespace WatchIt.WebAPI.Controllers;

[ApiController]
[Route("accounts")]
public class AccountsController(IAccountsBusinessLogic accountsBusinessLogic) : ControllerBase
{
    #region Main
    
    [HttpGet]
    [AllowAnonymous]
    [TranslateResultToActionResult]
    public async Task<Result<IEnumerable<AccountResponse>>> GetAccounts([FromQuery]AccountFilterQuery filterQuery, [FromQuery]OrderQuery orderQuery, [FromQuery]PagingQuery pagingQuery, [FromQuery(Name = "include_profile_pictures")]bool includeProfilePictures = false) => 
        await accountsBusinessLogic.GetAccounts(filterQuery, orderQuery, pagingQuery, includeProfilePictures);
    
    [HttpGet("{id:long}")]
    [AllowAnonymous]
    [TranslateResultToActionResult]
    public async Task<Result<AccountResponse>> GetAccount([FromRoute(Name = "id")] long id, [FromQuery(Name = "include_profile_pictures")]bool includeProfilePictures = false) => 
        await accountsBusinessLogic.GetAccount(id, includeProfilePictures);
    
    [HttpPost]
    [AllowAnonymous]
    [TranslateResultToActionResult]
    public async Task<Result<AccountResponse>> PostAccount([FromBody]AccountRequest body) =>
        await accountsBusinessLogic.PostAccount(body);
    
    #endregion
    
    #region Profile picture
    
    [HttpGet("{id:long}/profile_picture")]
    [AllowAnonymous]
    [TranslateResultToActionResult]
    public async Task<Result<ImageResponse>> GetAccountProfilePicture([FromRoute(Name = "id")] long id) => 
        await accountsBusinessLogic.GetAccountProfilePicture(id);

    [HttpPut("profile_picture")]
    [Authorize]
    [TranslateResultToActionResult]
    public async Task<Result<ImageResponse>> PutAccountProfilePicture([FromBody] ImageRequest body) => 
        await accountsBusinessLogic.PutAccountProfilePicture(body);

    [HttpDelete("profile_picture")]
    [Authorize]
    [TranslateResultToActionResult]
    public async Task<Result> DeleteAccountProfilePicture() => 
        await accountsBusinessLogic.DeleteAccountProfilePicture();

    #endregion
    
    #region Background picture
    
    [HttpGet("{id:long}/background_picture")]
    [AllowAnonymous]
    [TranslateResultToActionResult]
    public async Task<Result<PhotoResponse>> GetAccountBackgroundPicture([FromRoute(Name = "id")] long id) => 
        await accountsBusinessLogic.GetAccountBackgroundPicture(id);

    [HttpPut("background_picture")]
    [Authorize]
    [TranslateResultToActionResult]
    public async Task<Result<PhotoResponse>> PutAccountBackgroundPicture([FromBody] AccountBackgroundPictureRequest body) => 
        await accountsBusinessLogic.PutAccountBackgroundPicture(body);

    [HttpDelete("background_picture")]
    [Authorize]
    [TranslateResultToActionResult]
    public async Task<Result> DeleteAccountBackgroundPicture() => 
        await accountsBusinessLogic.DeleteAccountBackgroundPicture();

    #endregion
    
    #region Profile edit
    
    [HttpPatch("profile_info")]
    [Authorize]
    [TranslateResultToActionResult]
    public async Task<Result> PatchAccountProfileInfo([FromBody] AccountProfileInfoRequest body) => 
        await accountsBusinessLogic.PatchAccountProfileInfo(body);
    
    [HttpPatch("username")]
    [Authorize]
    [TranslateResultToActionResult]
    public async Task<Result> PatchAccountUsername([FromBody] AccountUsernameRequest body) => 
        await accountsBusinessLogic.PatchAccountUsername(body);
    
    [HttpPatch("email")]
    [Authorize]
    [TranslateResultToActionResult]
    public async Task<Result> PatchAccountEmail([FromBody] AccountEmailRequest body) => 
        await accountsBusinessLogic.PatchAccountEmail(body);
    
    [HttpPatch("password")]
    [Authorize]
    [TranslateResultToActionResult]
    public async Task<Result> PatchAccountPassword([FromBody] AccountPasswordRequest body) => 
        await accountsBusinessLogic.PatchAccountPassword(body);
    
    #endregion

    #region Log out
    
    [HttpDelete("logout")]
    [AllowAnonymous]
    [TranslateResultToActionResult]
    public async Task<Result> Logout([FromBody]AccountLogoutRequest body) => 
        await accountsBusinessLogic.Logout(body);
    
    [HttpDelete("logout_all")]
    [Authorize]
    [TranslateResultToActionResult]
    public async Task<Result> LogoutAll() => 
        await accountsBusinessLogic.LogoutAll();

    #endregion
    
    #region Follows
    
    [HttpGet("{id:long}/follows")]
    [AllowAnonymous]
    [TranslateResultToActionResult]
    public async Task<Result<IEnumerable<AccountResponse>>> GetAccountFollows([FromRoute(Name = "id")] long id, [FromQuery] AccountFilterQuery filterQuery, [FromQuery] OrderQuery orderQuery, [FromQuery] PagingQuery pagingQuery) => 
        await accountsBusinessLogic.GetAccountFollows(id, filterQuery, orderQuery, pagingQuery);
    
    [HttpGet("{id:long}/followers")]
    [AllowAnonymous]
    [TranslateResultToActionResult]
    public async Task<Result<IEnumerable<AccountResponse>>> GetAccountFollowers([FromRoute(Name = "id")] long id, [FromQuery] AccountFilterQuery filterQuery, [FromQuery] OrderQuery orderQuery, [FromQuery] PagingQuery pagingQuery) => 
        await accountsBusinessLogic.GetAccountFollowers(id, filterQuery, orderQuery, pagingQuery);
    
    [HttpPost("follows/{followed_account_id:long}")]
    [Authorize]
    [TranslateResultToActionResult]
    public async Task<Result> PostAccountFollow([FromRoute(Name = "followed_account_id")] long followedAccountId) => 
        await accountsBusinessLogic.PostAccountFollow(followedAccountId);
    
    [HttpDelete("follows/{followed_account_id:long}")]
    [Authorize]
    [TranslateResultToActionResult]
    public async Task<Result> DeleteAccountFollow([FromRoute(Name = "followed_account_id")] long followedAccountId) => 
        await accountsBusinessLogic.DeleteAccountFollow(followedAccountId);
    
    #endregion
    
    #region Ratings
    
    [HttpGet("{id:long}/media")]
    [AllowAnonymous]
    [TranslateResultToActionResult]
    public async Task<Result<IEnumerable<MediumUserRatedResponse>>> GetAccountRatedMedia([FromRoute(Name = "id")] long id, [FromQuery]MediumFilterQuery filterQuery, [FromQuery]MediumUserRatedFilterQuery<Medium> userRatedFilterQuery, [FromQuery]OrderQuery orderQuery, [FromQuery]PagingQuery pagingQuery, [FromQuery(Name = "include_pictures")]bool includePictures = false) => 
        await accountsBusinessLogic.GetAccountRatedMedia(id, filterQuery, userRatedFilterQuery, orderQuery, pagingQuery, includePictures);
    
    [HttpGet("{id:long}/media/movies")]
    [AllowAnonymous]
    [TranslateResultToActionResult]
    public async Task<Result<IEnumerable<MediumMovieUserRatedResponse>>> GetAccountRatedMediaMovies([FromRoute(Name = "id")] long id, [FromQuery]MediumMovieFilterQuery filterQuery, [FromQuery]MediumUserRatedFilterQuery<MediumMovie> userRatedFilterQuery, [FromQuery]OrderQuery orderQuery, [FromQuery]PagingQuery pagingQuery, [FromQuery(Name = "include_pictures")]bool includePictures = false) => 
        await accountsBusinessLogic.GetAccountRatedMediaMovies(id, filterQuery, userRatedFilterQuery, orderQuery, pagingQuery, includePictures);
    
    [HttpGet("{id:long}/media/series")]
    [AllowAnonymous]
    [TranslateResultToActionResult]
    public async Task<Result<IEnumerable<MediumSeriesUserRatedResponse>>> GetAccountRatedMediaSeries([FromRoute(Name = "id")] long id, [FromQuery]MediumSeriesFilterQuery filterQuery, [FromQuery]MediumUserRatedFilterQuery<MediumSeries> userRatedFilterQuery, [FromQuery]OrderQuery orderQuery, [FromQuery]PagingQuery pagingQuery, [FromQuery(Name = "include_pictures")]bool includePictures = false) => 
        await accountsBusinessLogic.GetAccountRatedMediaSeries(id, filterQuery, userRatedFilterQuery, orderQuery, pagingQuery, includePictures);
    
    [HttpGet("{id:long}/people")]
    [AllowAnonymous]
    [TranslateResultToActionResult]
    public async Task<Result<IEnumerable<PersonUserRatedResponse>>> GetAccountRatedPeople([FromRoute(Name = "id")] long id, [FromQuery]PersonFilterQuery filterQuery, [FromQuery]PersonUserRatedFilterQuery userRatedFilterQuery, [FromQuery]OrderQuery orderQuery, [FromQuery]PagingQuery pagingQuery, [FromQuery(Name = "include_pictures")]bool includePictures = false) => 
        await accountsBusinessLogic.GetAccountRatedPeople(id, filterQuery, userRatedFilterQuery, orderQuery, pagingQuery, includePictures);
    
    #endregion
}