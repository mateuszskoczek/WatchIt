﻿using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SimpleToolkit.Extensions;
using WatchIt.Common.Model.Accounts;
using WatchIt.Database;
using WatchIt.Database.Model.Account;
using WatchIt.WebAPI.Services.Controllers.Common;
using WatchIt.WebAPI.Services.Utility.Tokens;
using WatchIt.WebAPI.Services.Utility.Tokens.Exceptions;
using WatchIt.WebAPI.Services.Utility.User;
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

    #endregion



    #region PRIVATE METHODS

    protected byte[] ComputeHash(string password, string leftSalt, string rightSalt) => SHA512.HashData(Encoding.UTF8.GetBytes($"{leftSalt}{password}{rightSalt}"));

    #endregion
}