using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SimpleToolkit.Extensions;
using WatchIt.Common.Model.Accounts;
using WatchIt.Common.Model.Media;
using WatchIt.Common.Model.Movies;
using WatchIt.Common.Model.Series;
using WatchIt.Database;
using WatchIt.Database.Model.Account;
using WatchIt.Database.Model.Media;
using WatchIt.Database.Model.Rating;
using WatchIt.WebAPI.Services.Controllers.Common;
using WatchIt.WebAPI.Services.Utility.Tokens;
using WatchIt.WebAPI.Services.Utility.Tokens.Exceptions;
using WatchIt.WebAPI.Services.Utility.User;
using Account = WatchIt.Database.Model.Account.Account;
using AccountProfilePicture = WatchIt.Common.Model.Accounts.AccountProfilePicture;

namespace WatchIt.WebAPI.Services.Controllers.Accounts;

public class AccountsControllerService(
    ILogger<AccountsControllerService> logger,
    DatabaseContext database,
    ITokensService tokensService,
    IUserService userService
) : IAccountsControllerService
{
    #region PUBLIC METHODS

    public async Task<RequestResult> Register(RegisterRequest data)
    {
        string leftSalt = StringExtensions.CreateRandom(20);
        string rightSalt = StringExtensions.CreateRandom(20);
        byte[] hash = ComputeHash(data.Password, leftSalt, rightSalt);

        Account account = new Account
        {
            Username = data.Username,
            Email = data.Email,
            Password = hash,
            LeftSalt = leftSalt,
            RightSalt = rightSalt,
        };
        await database.Accounts.AddAsync(account);
        await database.SaveChangesAsync();
        
        logger.LogInformation($"New account with ID {account.Id} was created (username: {account.Username}; email: {account.Email})");
        return RequestResult.Created($"accounts/{account.Id}", new RegisterResponse(account));
    }

    public async Task<RequestResult> Authenticate(AuthenticateRequest data)
    {
        Account? account = await database.Accounts.FirstOrDefaultAsync(x => string.Equals(x.Email, data.UsernameOrEmail) || string.Equals(x.Username, data.UsernameOrEmail));
        if (account is null || !ComputeHash(data.Password, account.LeftSalt, account.RightSalt).SequenceEqual(account.Password))
        {
            return RequestResult.Unauthorized();
        }
        
        Task<string> refreshTokenTask = tokensService.CreateRefreshTokenAsync(account, true);
        Task<string> accessTokenTask = tokensService.CreateAccessTokenAsync(account);
        AuthenticateResponse response = new AuthenticateResponse
        {
            AccessToken = await accessTokenTask,
            RefreshToken = await refreshTokenTask,
        };
        
        account.LastActive = DateTime.UtcNow;
        await database.SaveChangesAsync();
        
        logger.LogInformation($"Account with ID {account.Id} was authenticated");
        return RequestResult.Ok(response);
    }

    public async Task<RequestResult> AuthenticateRefresh()
    {
        Guid jti = userService.GetJti();
        AccountRefreshToken? token = await database.AccountRefreshTokens.FirstOrDefaultAsync(x => x.Id == jti);
        if (token is null || token.ExpirationDate < DateTime.UtcNow)
        {
            return RequestResult.Unauthorized();
        }

        string refreshToken;
        try
        {
            refreshToken = await tokensService.ExtendRefreshTokenAsync(token.Account, token.Id);
        }
        catch (TokenNotFoundException)
        {
            return RequestResult.Unauthorized();
        }
        catch (TokenNotExtendableException)
        {
            refreshToken = userService.GetRawToken().Replace("Bearer ", string.Empty);
        }
        
        string accessToken = await tokensService.CreateAccessTokenAsync(token.Account);
        
        token.Account.LastActive = DateTime.UtcNow;
        await database.SaveChangesAsync();
        
        logger.LogInformation($"Account with ID {token.AccountId} was authenticated by token refreshing");
        return RequestResult.Ok(new AuthenticateResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
        });
    }

    public async Task<RequestResult> Logout()
    {
        Guid jti = userService.GetJti();
        AccountRefreshToken? token = await database.AccountRefreshTokens.FirstOrDefaultAsync(x => x.Id == jti);
        if (token is not null)
        {
            database.AccountRefreshTokens.Attach(token);
            database.AccountRefreshTokens.Remove(token);
            await database.SaveChangesAsync();
        }
        return RequestResult.NoContent();
    }

    public async Task<RequestResult> GetAccountProfilePicture(long id)
    {
        Account? account = await database.Accounts.FirstOrDefaultAsync(x => x.Id == id);
        if (account is null)
        {
            return RequestResult.BadRequest()
                                .AddValidationError("id", "Account with this id does not exists");
        }

        if (account.ProfilePicture is null)
        {
            return RequestResult.NotFound();
        }
        
        AccountProfilePictureResponse picture = new AccountProfilePictureResponse(account.ProfilePicture);
        return RequestResult.Ok(picture);
    }

    public async Task<RequestResult> GetAccountInfo(long id)
    {
        Account? account = await database.Accounts.FirstOrDefaultAsync(x => x.Id == id);
        if (account is null)
        {
            return RequestResult.NotFound();
        }
        
        AccountResponse response = new AccountResponse(account);
        return RequestResult.Ok(response);
    }

    public async Task<RequestResult> PutAccountInfo(AccountRequest data)
    {
        Account? account = await database.Accounts.FirstOrDefaultAsync(x => x.Id == userService.GetUserId());
        if (account is null)
        {
            return RequestResult.NotFound();
        }
        
        data.UpdateAccount(account);
        return RequestResult.Ok();
    }
    
    public async Task<RequestResult> GetAccountRatedMovies(long id, MovieRatedQueryParameters query)
    {
        Account? account = await database.Accounts.FirstOrDefaultAsync(x => x.Id == id);
        if (account is null)
        {
            return RequestResult.NotFound();
        }

        IEnumerable<MovieRatedResponse> response = account.RatingMedia.Join(database.MediaMovies, x => x.MediaId, x => x.Id, (x, y) => new MovieRatedResponse(y, x));
        response = query.PrepareData(response);
        return RequestResult.Ok(response);
    }
    
    public async Task<RequestResult> GetAccountRatedSeries(long id, SeriesRatedQueryParameters query)
    {
        Account? account = await database.Accounts.FirstOrDefaultAsync(x => x.Id == id);
        if (account is null)
        {
            return RequestResult.NotFound();
        }

        IEnumerable<SeriesRatedResponse> response = account.RatingMedia.Join(database.MediaSeries, x => x.MediaId, x => x.Id, (x, y) => new SeriesRatedResponse(y, x));
        response = query.PrepareData(response);
        return RequestResult.Ok(response);
    }

    #endregion



    #region PRIVATE METHODS

    protected byte[] ComputeHash(string password, string leftSalt, string rightSalt) => SHA512.HashData(Encoding.UTF8.GetBytes($"{leftSalt}{password}{rightSalt}"));

    #endregion
}