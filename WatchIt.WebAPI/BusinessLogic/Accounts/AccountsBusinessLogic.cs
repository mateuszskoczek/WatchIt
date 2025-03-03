using Ardalis.Result;
using Microsoft.EntityFrameworkCore;
using WatchIt.Database.Model.Accounts;
using WatchIt.Database.Model.Media;
using WatchIt.Database.Model.People;
using WatchIt.DTO.Models.Controllers.Accounts;
using WatchIt.DTO.Models.Controllers.Accounts.Account;
using WatchIt.DTO.Models.Controllers.Accounts.AccountBackgroundPicture;
using WatchIt.DTO.Models.Controllers.Accounts.AccountEmail;
using WatchIt.DTO.Models.Controllers.Accounts.AccountLogout;
using WatchIt.DTO.Models.Controllers.Accounts.AccountPassword;
using WatchIt.DTO.Models.Controllers.Accounts.AccountProfileInfo;
using WatchIt.DTO.Models.Controllers.Accounts.AccountUsername;
using WatchIt.DTO.Models.Controllers.Media;
using WatchIt.DTO.Models.Controllers.Media.Medium.Query;
using WatchIt.DTO.Models.Controllers.Media.Medium.Response;
using WatchIt.DTO.Models.Controllers.People;
using WatchIt.DTO.Models.Controllers.People.Person;
using WatchIt.DTO.Models.Controllers.People.Person.Query;
using WatchIt.DTO.Models.Controllers.Photos;
using WatchIt.DTO.Models.Controllers.Photos.Photo;
using WatchIt.DTO.Models.Generics.Image;
using WatchIt.DTO.Query;
using WatchIt.WebAPI.Helpers;
using WatchIt.WebAPI.Repositories.Accounts;
using WatchIt.WebAPI.Repositories.Media;
using WatchIt.WebAPI.Repositories.People;
using WatchIt.WebAPI.Services.Tokens;
using WatchIt.WebAPI.Services.User;

namespace WatchIt.WebAPI.BusinessLogic.Accounts;

public class AccountsBusinessLogic : IAccountsBusinessLogic
{
    #region SERVICES

    private readonly IUserService _userService;
    private readonly ITokensService _tokensService;
    private readonly IAccountsRepository _accountRepository;
    private readonly IMediaRepository _mediaRepository;
    private readonly IPeopleRepository _peopleRepository;

    #endregion
    
    
    
    #region CONSTRUCTORS
    
    public AccountsBusinessLogic(IUserService userService, ITokensService tokensService, IAccountsRepository accountRepository, IMediaRepository mediaRepository, IPeopleRepository peopleRepository)
    {
        _userService = userService;
        _tokensService = tokensService;
        _accountRepository = accountRepository;
        _mediaRepository = mediaRepository;
        _peopleRepository = peopleRepository;
    }
    
    #endregion
    
    
    
    #region PUBLIC METHODS
    
    #region Main
    
    public async Task<Result<IEnumerable<AccountResponse>>> GetAccounts(AccountFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, bool includeProfilePictures)
    {
        IEnumerable<Account> entities = await _accountRepository.GetAllAsync(x => IncludeForAccountResponse(x, includeProfilePictures));
        return Result.Success(entities.Select(x => x.ToResponse()));
    }
    
    public async Task<Result<AccountResponse>> GetAccount(long accountId, bool includeProfilePictures)
    {
        Account? account = await _accountRepository.GetAsync(accountId, x => IncludeForAccountResponse(x, includeProfilePictures));
        return account switch
        {
            null => Result.NotFound(),
            _ => Result.Success(account.ToResponse())
        };
    }

    public async Task<Result<AccountResponse>> PostAccount(AccountRequest body)
    {
        Account entity = body.ToEntity(PasswordHelpers.GeneratePasswordData);
        await _accountRepository.AddAsync(entity);
        AccountResponse response = entity.ToResponse();
        return Result<AccountResponse>.Created(response);
    }
    
    #endregion
    
    #region Profile picture
    
    public async Task<Result<ImageResponse>> GetAccountProfilePicture(long accountId)
    {
        AccountProfilePicture? picture = await _accountRepository.GetProfilePictureAsync(accountId);
        return picture switch
        {
            null => Result.NotFound(),
            _ => Result.Success(picture.ToResponse())
        };
    }
    
    public async Task<Result<ImageResponse>> PutAccountProfilePicture(ImageRequest body)
    {
        Account account = await _userService.GetAccountAsync();
        AccountProfilePicture entity = await _accountRepository.UpdateOrAddProfilePictureAsync(account.Id, () => AccountsMappers.ToEntity(body, account.Id), x => x.UpdateWithRequest(body));
        return Result.Success(entity.ToResponse());
    }
    
    public async Task<Result> DeleteAccountProfilePicture()
    {
        Account account = await _userService.GetAccountAsync();
        await _accountRepository.DeleteProfilePictureAsync(account.Id);
        return Result.NoContent();
    }
    
    #endregion

    #region Profile background

    public async Task<Result<PhotoResponse>> GetAccountBackgroundPicture(long accountId)
    {
        AccountBackgroundPicture? picture = await _accountRepository.GetBackgroundPictureAsync(accountId, x => x.Include(y => y.Background)
                                                                                                                .ThenInclude(y => y.Photo));
        return picture switch
        {
            null => Result.NotFound(),
            _ => Result.Success(picture.Background.Photo.ToResponse())
        };
    }
    
    public async Task<Result<PhotoResponse>> PutAccountBackgroundPicture(AccountBackgroundPictureRequest body)
    {
        Account account = await _userService.GetAccountAsync();
        AccountBackgroundPicture entity = await _accountRepository.UpdateOrAddBackgroundPictureAsync(account.Id, () => body.ToEntity(account.Id), x => x.UpdateWithRequest(body));
        PhotoResponse photo = await GetAccountBackgroundPicture(account.Id);
        return Result.Success(photo);
    }
    
    public async Task<Result> DeleteAccountBackgroundPicture()
    {
        Account account = await _userService.GetAccountAsync();
        await _accountRepository.DeleteBackgroundPictureAsync(account.Id);
        return Result.NoContent();
    }

    #endregion
    
    #region Profile edit
    
    public async Task<Result> PatchAccountProfileInfo(AccountProfileInfoRequest body)
    {
        Account account = await _userService.GetAccountAsync();
        await _accountRepository.UpdateAsync(account, x => x.UpdateWithRequest(body));
        return Result.Success();
    }
    
    public async Task<Result> PatchAccountUsername(AccountUsernameRequest body)
    {
        Account account = await _userService.GetAccountAsync();
        if (!PasswordHelpers.ValidatePassword(body.Password, account.GetPasswordData()))
        {
            return Result.Unauthorized();
        }
        await _accountRepository.UpdateAsync(account, x => x.UpdateWithRequest(body));
        return Result.Success();
    }
    
    public async Task<Result> PatchAccountEmail(AccountEmailRequest body)
    {
        Account account = await _userService.GetAccountAsync();
        if (!PasswordHelpers.ValidatePassword(body.Password, account.GetPasswordData()))
        {
            return Result.Unauthorized();
        }
        await _accountRepository.UpdateAsync(account, x => x.UpdateWithRequest(body));
        return Result.Success();
    }
    
    public async Task<Result> PatchAccountPassword(AccountPasswordRequest body)
    {
        Account account = await _userService.GetAccountAsync();
        if (!PasswordHelpers.ValidatePassword(body.Password, account.GetPasswordData()))
        {
            return Result.Unauthorized();
        }
        await _accountRepository.UpdateAsync(account, x => x.UpdateWithRequest(body, PasswordHelpers.GeneratePasswordData));
        return Result.Success();
    }
    
    #endregion
    
    #region Log out
    
    public async Task<Result> Logout(AccountLogoutRequest body)
    {
        if (body.RefreshToken is not null)
        {
            await _tokensService.RevokeRefreshTokenAsync(body.RefreshToken);
        }
        return Result.NoContent();
    }

    public async Task<Result> LogoutAll()
    {
        Account accountEntity = await _userService.GetAccountAsync();
        await _tokensService.RevokeAccountRefreshTokensAsync(accountEntity);
        return Result.NoContent();
    }
    
    #endregion
    
    #region Follows
    
    public async Task<Result<IEnumerable<AccountResponse>>> GetAccountFollows(long accountId, AccountFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery)
    {
        if (!await _accountRepository.ExistsAsync(accountId))
        {
            return Result.NotFound();
        }
        IEnumerable<Account> accounts = await _accountRepository.GetFollowsAsync(accountId, filterQuery, orderQuery, pagingQuery, x => x.Include(y => y.Gender));
        return Result.Success(accounts.Select(x => x.ToResponse()));
    }
    
    public async Task<Result<IEnumerable<AccountResponse>>> GetAccountFollowers(long accountId, AccountFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery)
    {
        if (!await _accountRepository.ExistsAsync(accountId))
        {
            return Result.NotFound();
        }
        IEnumerable<Account> accounts = await _accountRepository.GetFollowersAsync(accountId, filterQuery, orderQuery, pagingQuery, x => x.Include(y => y.Gender));
        return Result.Success(accounts.Select(x => x.ToResponse()));
    }
    
    public async Task<Result> PostAccountFollow(long followedAccountId)
    {
        Account account = await _userService.GetAccountAsync();
        if (account.Id == followedAccountId)
        {
            return Result.Error("You cannot follow yourself");
        }
        if (!await _accountRepository.ExistsAsync(followedAccountId))
        {
            return Result.Error("User with this id doesn't exist");
        }
        if (!await _accountRepository.FollowExistsAsync(account.Id, followedAccountId))
        {
            await _accountRepository.AddFollowAsync(AccountsMappers.CreateAccountFollowEntity(account.Id, followedAccountId));
        }
        return Result.Success();
    }
    
    public async Task<Result> DeleteAccountFollow(long followedAccountId)
    {
        Account account = await _userService.GetAccountAsync();
        await _accountRepository.DeleteFollowAsync(account.Id, followedAccountId);
        return Result.NoContent();
    }
    
    #endregion

    #region Ratings

    public async Task<Result<IEnumerable<MediumUserRatedResponse>>> GetAccountRatedMedia(long accountId, MediumFilterQuery filterQuery, MediumUserRatedFilterQuery<Medium> userRatedFilterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, bool includePictures)
    {
        if (!await _accountRepository.ExistsAsync(accountId))
        {
            return Result.NotFound();
        }

        IEnumerable<Medium> media = await _mediaRepository.GetAllRatedByAccountAsync(accountId, filterQuery, userRatedFilterQuery, orderQuery, pagingQuery, x => IncludeForMediumResponse(x, includePictures));
        return Result.Success(media.Select(x => x.ToResponse(accountId)));
    }
    
    public async Task<Result<IEnumerable<MediumMovieUserRatedResponse>>> GetAccountRatedMediaMovies(long accountId, MediumMovieFilterQuery filterQuery, MediumUserRatedFilterQuery<MediumMovie> userRatedFilterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, bool includePictures)
    {
        if (!await _accountRepository.ExistsAsync(accountId))
        {
            return Result.NotFound();
        }

        IEnumerable<MediumMovie> mediumMovies = await _mediaRepository.GetAllMoviesRatedByAccountAsync(accountId, filterQuery, userRatedFilterQuery, orderQuery, pagingQuery, x => IncludeForMediumResponse(x, includePictures));
        return Result.Success(mediumMovies.Select(x => x.ToResponse(accountId)));
    }
    
    public async Task<Result<IEnumerable<MediumSeriesUserRatedResponse>>> GetAccountRatedMediaSeries(long accountId, MediumSeriesFilterQuery filterQuery, MediumUserRatedFilterQuery<MediumSeries> userRatedFilterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, bool includePictures)
    {
        if (!await _accountRepository.ExistsAsync(accountId))
        {
            return Result.NotFound();
        }

        IEnumerable<MediumSeries> mediumSeries = await _mediaRepository.GetAllSeriesRatedByAccountAsync(accountId, filterQuery, userRatedFilterQuery, orderQuery, pagingQuery, x => IncludeForMediumResponse(x, includePictures));
        return Result.Success(mediumSeries.Select(x => x.ToResponse(accountId)));
    }
    
    public async Task<Result<IEnumerable<PersonUserRatedResponse>>> GetAccountRatedPeople(long accountId, PersonFilterQuery filterQuery, PersonUserRatedFilterQuery userRatedFilterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, bool includePictures)
    {
        if (!await _accountRepository.ExistsAsync(accountId))
        {
            return Result.NotFound();
        }

        IEnumerable<Person> people = await _peopleRepository.GetAllRatedByAccountAsync(accountId, filterQuery, userRatedFilterQuery, orderQuery, pagingQuery, x => IncludeForPersonResponse(x, includePictures));
        return Result.Success(people.Select(x => x.ToResponse(accountId)));
    }

    #endregion
    
    #endregion
    
    
    
    #region PRIVATE METHODS

    private IQueryable<Account> IncludeForAccountResponse(IQueryable<Account> query, bool includeProfilePictures)
    {
        query = query.Include(y => y.Gender);
        if (includeProfilePictures)
        {
            query = query.Include(y => y.ProfilePicture);
        }
        return query;
    }
    
    private IQueryable<T> IncludeForMediumResponse<T>(IQueryable<T> query, bool includePictures) where T : Medium
    {
        query = query.Include(y => y.Genres)
                     .Include(y => y.Ratings)
                     .Include(y => y.ViewCounts);
        if (includePictures)
        {
            query = query.Include(y => y.Picture);
        }
        return query;
    }
    
    private IQueryable<Person> IncludeForPersonResponse(IQueryable<Person> query, bool includeProfilePictures)
    {
        query = query.Include(y => y.Gender)
                     .Include(y => y.Roles)
                     .ThenInclude(y => y.Ratings)
                     .Include(y => y.ViewCounts);
        if (includeProfilePictures)
        {
            query = query.Include(y => y.Picture);
        }
        return query;
    }
    
    #endregion
}