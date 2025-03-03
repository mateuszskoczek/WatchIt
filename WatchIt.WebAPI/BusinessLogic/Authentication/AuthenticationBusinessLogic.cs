using Ardalis.Result;
using WatchIt.Database.Model.Accounts;
using WatchIt.DTO.Models.Controllers.Accounts;
using WatchIt.DTO.Models.Controllers.Authentication;
using WatchIt.WebAPI.Helpers;
using WatchIt.WebAPI.Repositories.Accounts;
using WatchIt.WebAPI.Services.Tokens;
using WatchIt.WebAPI.Services.User;

namespace WatchIt.WebAPI.BusinessLogic.Authentication;

public class AuthenticationBusinessLogic : IAuthenticationBusinessLogic
{
    #region SERVICES

    private readonly ITokensService _tokensService;
    private readonly IUserService _userService;
    private readonly IAccountsRepository _accountRepository;

    #endregion
    
    
    
    #region CONSTRUCTORS
    
    public AuthenticationBusinessLogic(ITokensService tokensService, IUserService userService, IAccountsRepository accountRepository)
    {
        _tokensService = tokensService;
        _userService = userService;
        _accountRepository = accountRepository;
    }
    
    #endregion
    
    
    
    #region PUBLIC METHODS

    public async Task<Result<AuthenticationResponse>> Authenticate(AuthenticationRequest body)
    {
        Account? accountEntity = await _accountRepository.GetByUsernameOrEmailAsync(body.UsernameOrEmail);
        if (accountEntity is null || !PasswordHelpers.ValidatePassword(body.Password, accountEntity.GetPasswordData()))
        {
            return Result.Unauthorized();
        }

        string accessToken = _tokensService.CreateAccessToken(accountEntity);
        string refreshToken = await _tokensService.CreateRefreshTokenAsync(accountEntity, body.RememberMe);
        AuthenticationResponse response = AuthenticationMappers.CreateAuthenticationResponse(accessToken, refreshToken);

        await _accountRepository.UpdateAsync(accountEntity, x => x.UpdateActiveDate());
        
        return Result.Success(response);
    }

    public async Task<Result<AuthenticationResponse>> AuthenticateRefresh(AuthenticationRefreshRequest body)
    {
        Account accountEntity = await _tokensService.ExtendRefreshTokenAsync(body.RefreshToken, body.AccessToken);
        string accessToken = _tokensService.CreateAccessToken(accountEntity);
        AuthenticationResponse response = AuthenticationMappers.CreateAuthenticationResponse(accessToken, body.RefreshToken);

        await _accountRepository.UpdateAsync(accountEntity, x => x.UpdateActiveDate());
        
        return Result.Success(response);
    }
    
    #endregion
}