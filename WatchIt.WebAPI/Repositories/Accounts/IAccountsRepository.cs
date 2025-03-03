using Microsoft.EntityFrameworkCore;
using WatchIt.Database.Model.Accounts;
using WatchIt.DTO.Models.Controllers.Accounts.Account;
using WatchIt.DTO.Query;

namespace WatchIt.WebAPI.Repositories.Accounts;

public interface IAccountsRepository : IRepository<Account>
{
    Task<bool> ExistsAsync(long id);
    Task<Account?> GetAsync(long id, Func<IQueryable<Account>, IQueryable<Account>>? additionalIncludes = null);
    Task<Account?> GetByUsernameOrEmailAsync(string usernameOrEmail, Func<IQueryable<Account>, IQueryable<Account>>? additionalIncludes = null);
    Task<IEnumerable<Account>> GetAllAsync(AccountFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, Func<IQueryable<Account>, IQueryable<Account>>? additionalIncludes = null);

    Task<AccountProfilePicture?> GetProfilePictureAsync(long id, Func<IQueryable<AccountProfilePicture>, IQueryable<AccountProfilePicture>>? additionalIncludes = null);
    Task<AccountProfilePicture> UpdateOrAddProfilePictureAsync(long id, Func<AccountProfilePicture> addFunc, Action<AccountProfilePicture> updateFunc);
    Task DeleteProfilePictureAsync(long id);

    Task<bool> FollowExistsAsync(long followerId, long followedId);
    Task<IEnumerable<Account>> GetFollowsAsync(long id, AccountFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, Func<IQueryable<Account>, IQueryable<Account>>? additionalIncludes = null);
    Task<IEnumerable<Account>> GetFollowersAsync(long id, AccountFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, Func<IQueryable<Account>, IQueryable<Account>>? additionalIncludes = null);
    Task AddFollowAsync(AccountFollow entity);
    Task DeleteFollowAsync(long followerId, long followedId);

    Task<AccountBackgroundPicture?> GetBackgroundPictureAsync(long id, Func<IQueryable<AccountBackgroundPicture>, IQueryable<AccountBackgroundPicture>>? additionalIncludes = null);
    Task<AccountBackgroundPicture> UpdateOrAddBackgroundPictureAsync(long id, Func<AccountBackgroundPicture> addFunc, Action<AccountBackgroundPicture> updateFunc);
    Task DeleteBackgroundPictureAsync(long id);

    Task UpdateRefreshTokenAsync(AccountRefreshToken refreshToken, Action<AccountRefreshToken> updateFunc);
    Task AddRefreshTokenAsync(AccountRefreshToken refreshToken);
    Task DeleteRefreshTokenAsync(Guid refreshTokenId);
    Task DeleteUserRefreshTokensAsync(long id);
}