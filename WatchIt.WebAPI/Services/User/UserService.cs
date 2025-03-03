using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using WatchIt.Database.Model.Accounts;
using WatchIt.WebAPI.Constants;
using WatchIt.WebAPI.Repositories.Accounts;
using WatchIt.WebAPI.Services.Tokens;

namespace WatchIt.WebAPI.Services.User;

public class UserService : IUserService
{
    #region SERVICES
    
    private readonly IHttpContextAccessor _accessor;
    private readonly IAccountsRepository _accountsRepository;

    #endregion



    #region CONSTRUCTORS

    public UserService(IHttpContextAccessor accessor, IAccountsRepository accountsRepository)
    {
        _accessor = accessor;
        _accountsRepository = accountsRepository;
    }

    #endregion
    
    
    
    #region PUBLIC METHODS

    public async Task<Account> GetAccountAsync()
    {
        long? id = GetAccountId();
        if (!id.HasValue)
        {
            throw new SecurityTokenException("Incorrect sub claim");
        }
        
        Account? account = await _accountsRepository.GetAsync(id.Value);
        if (account is null)
        {
            throw new SecurityTokenException("Account with sub claim id not found");
        }
        return account;
    }

    #endregion



    #region PRIVATE METHODS

    private ClaimsPrincipal? GetClaims()
    {
        if (_accessor.HttpContext is null)
        {
            throw new NullReferenceException();
        }
        return _accessor.HttpContext.User;
    }
    
    private long? GetAccountId()
    {
        ClaimsPrincipal? user = GetClaims();
        Claim? subClaim = user?.FindFirst(JwtRegisteredClaimNames.Sub);
        if (subClaim is null)
        {
            return null;
        }
        long.TryParse(subClaim.Value, out long accountId);
        return accountId;
    }

    #endregion
}