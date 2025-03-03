using Microsoft.EntityFrameworkCore;
using WatchIt.Database;
using WatchIt.Database.Model.Accounts;
using WatchIt.DTO.Models.Controllers.Accounts.Account;
using WatchIt.DTO.Query;

namespace WatchIt.WebAPI.Repositories.Accounts;

public class AccountsRepository : Repository<Account>, IAccountsRepository
{
    #region CONSTRUCTORS
    
    public AccountsRepository(DatabaseContext database) : base(database) {}
    
    #endregion
    
    
    
    #region PUBLIC METHODS
    
    #region Main
    
    public async Task<bool> ExistsAsync(long id) =>
        await DefaultSet.AnyAsync(x => x.Id == id);

    public async Task<Account?> GetAsync(long id, Func<IQueryable<Account>, IQueryable<Account>>? additionalIncludes = null) =>
        await DefaultSet.Include(additionalIncludes)
                        .FirstOrDefaultAsync(x => x.Id == id);
    
    public async Task<Account?> GetByUsernameOrEmailAsync(string usernameOrEmail, Func<IQueryable<Account>, IQueryable<Account>>? additionalIncludes = null) =>
        await DefaultSet.Include(additionalIncludes)
                        .FirstOrDefaultAsync(x => x.Username == usernameOrEmail || x.Email == usernameOrEmail);
    
    public async Task<IEnumerable<Account>> GetAllAsync(AccountFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, Func<IQueryable<Account>, IQueryable<Account>>? additionalIncludes = null) =>
        await DefaultSet.ApplyFilter(filterQuery)
                        .ApplyOrder(orderQuery, AccountOrderKeys.Base)
                        .ApplyPaging(pagingQuery)
                        .Include(additionalIncludes)
                        .ToListAsync();
    
    #endregion
    
    #region Profile picture
    
    public async Task<AccountProfilePicture?> GetProfilePictureAsync(long id, Func<IQueryable<AccountProfilePicture>, IQueryable<AccountProfilePicture>>? additionalIncludes = null) =>
        await Database.AccountProfilePictures
                      .Include(additionalIncludes)
                      .FirstOrDefaultAsync(x => x.AccountId == id);

    public async Task<AccountProfilePicture> UpdateOrAddProfilePictureAsync(long id, Func<AccountProfilePicture> addFunc, Action<AccountProfilePicture> updateFunc) =>
        await UpdateOrAddAsync(await GetProfilePictureAsync(id), addFunc, updateFunc);

    public async Task DeleteProfilePictureAsync(long id) =>
        await DeleteAsync<AccountProfilePicture>(x => x.AccountId == id);
    
    #endregion

    #region Background picture

    public async Task<AccountBackgroundPicture?> GetBackgroundPictureAsync(long id, Func<IQueryable<AccountBackgroundPicture>, IQueryable<AccountBackgroundPicture>>? additionalIncludes = null) =>
        await Database.AccountBackgroundPictures
                      .Include(additionalIncludes)
                      .FirstOrDefaultAsync(x => x.AccountId == id);
    
    public async Task<AccountBackgroundPicture> UpdateOrAddBackgroundPictureAsync(long id, Func<AccountBackgroundPicture> addFunc, Action<AccountBackgroundPicture> updateFunc) =>
        await UpdateOrAddAsync(await GetBackgroundPictureAsync(id, x => x.Include(y => y.Background)
                                                                         .ThenInclude(y => y.Photo)), addFunc, updateFunc);
    
    public async Task DeleteBackgroundPictureAsync(long id) =>
        await DeleteAsync<AccountBackgroundPicture>(x => x.AccountId == id);

    #endregion
    
    #region Follow
        
    public async Task<bool> FollowExistsAsync(long followerId, long followedId) =>
        await Database.AccountFollows
                      .AnyAsync(x => x.FollowerId == followerId && x.FollowedId == followedId);

    public async Task<IEnumerable<Account>> GetFollowsAsync(long id, AccountFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, Func<IQueryable<Account>, IQueryable<Account>>? additionalIncludes = null) =>
        await Database.Accounts
                      .Where(x => x.Followers
                                   .Any(y => y.Id == id))
                      .ApplyFilter(filterQuery)
                      .ApplyOrder(orderQuery, AccountOrderKeys.Base)
                      .ApplyPaging(pagingQuery)
                      .Include(additionalIncludes)
                      .ToListAsync();
    
    public async Task<IEnumerable<Account>> GetFollowersAsync(long id, AccountFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, Func<IQueryable<Account>, IQueryable<Account>>? additionalIncludes = null) =>
        await Database.Accounts
                      .Where(x => x.Follows
                                   .Any(y => y.Id == id))
                      .ApplyFilter(filterQuery)
                      .ApplyOrder(orderQuery, AccountOrderKeys.Base)
                      .ApplyPaging(pagingQuery)
                      .Include(additionalIncludes)
                      .ToListAsync();

    public async Task AddFollowAsync(AccountFollow entity) => 
        await AddAsync(entity);
    
    public async Task DeleteFollowAsync(long followerId, long followedId) =>
        await DeleteAsync<AccountFollow>(x => x.FollowerId == followerId && x.FollowedId == followedId);

    #endregion
    
    #region Refresh token
    
    public async Task UpdateRefreshTokenAsync(AccountRefreshToken refreshToken, Action<AccountRefreshToken> updateFunc) =>
        await UpdateAsync(refreshToken, updateFunc);
    
    public async Task AddRefreshTokenAsync(AccountRefreshToken refreshToken) =>
        await AddAsync(refreshToken);
    
    public async Task DeleteRefreshTokenAsync(Guid refreshTokenId) =>
        await DeleteAsync<AccountRefreshToken>(x => x.Token == refreshTokenId);
    
    public async Task DeleteUserRefreshTokensAsync(long id)
    {
        IQueryable<AccountRefreshToken> tokens = Database.AccountRefreshTokens.Where(x => x.AccountId == id);
        Database.AccountRefreshTokens.AttachRange(tokens);
        Database.AccountRefreshTokens.RemoveRange(tokens);
        await Database.SaveChangesAsync();
    }

    #endregion

    #endregion
}