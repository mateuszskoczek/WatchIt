using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WatchIt.Database.Model.Accounts;
using WatchIt.DTO.Models.Controllers.Accounts;
using WatchIt.WebAPI.Constants;
using WatchIt.WebAPI.Repositories.Accounts;
using WatchIt.WebAPI.Services.Tokens.Configuration;
using WatchIt.WebAPI.Services.Tokens.Exceptions;

namespace WatchIt.WebAPI.Services.Tokens;

public class TokensService : ITokensService
{
    #region SERVICES

    private readonly JWT _configuration;
    private readonly IAccountsRepository _accountsRepository;
    
    #endregion
    
    
    
    #region CONSTRUCTORS

    public TokensService(IConfiguration configuration, IAccountsRepository accountsRepository)
    {
        _configuration = configuration.GetSection("Authentication").GetSection("JWT").Get<JWT>()!;
        _accountsRepository = accountsRepository;
    }
    
    #endregion
    
    
    
    #region PUBLIC METHODS

    public string CreateAccessToken(Account account)
    {
        int lifetime = _configuration.Lifetime.AccessToken.Normal;
        DateTimeOffset expirationDate = new DateTimeOffset(DateTime.UtcNow.AddMinutes(lifetime));
        
        SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, account.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, expirationDate.Ticks.ToString()),
                new Claim(AdditionalClaimNames.Admin, account.IsAdmin.ToString())
            ]),
            Issuer = _configuration.Issuer,
            Audience = _configuration.Audience,
            Expires = expirationDate.UtcDateTime,
            SigningCredentials = new SigningCredentials(CreateSecurityKey(), _configuration.Algorithm),
        };
        
        return descriptor.ToJwtString();
    }

    public async Task<string> CreateRefreshTokenAsync(Account account, bool isExtendable)
    {
        Guid newToken = Guid.NewGuid();
        DateTimeOffset expirationDate = GetExpirationDate(_configuration.Lifetime.RefreshToken, isExtendable);

        AccountRefreshToken tokenEntity = AccountsMappers.CreateAccountRefreshTokenEntity(newToken, account.Id, expirationDate, isExtendable);
        await _accountsRepository.AddRefreshTokenAsync(tokenEntity);
        
        return Convert.ToBase64String(newToken.ToByteArray());
    }

    public async Task<Account> ExtendRefreshTokenAsync(string refreshToken, string accessToken)
    {
        long accountId = ValidateExpiredAccessTokenAndGetAccountId(accessToken);
        Account? account = await _accountsRepository.GetAsync(accountId, x => x.Include(y => y.RefreshTokens));
        if (account is null)
        {
            throw new SecurityTokenException("Invalid token");
        }
        
        Guid token = new Guid(Convert.FromBase64String(refreshToken));
        AccountRefreshToken? tokenEntity = account.RefreshTokens.FirstOrDefault(x => x.Token == token);
        if (tokenEntity is null)
        {
            throw new SecurityTokenException("Invalid token");
        }
        if (tokenEntity.ExpirationDate < DateTimeOffset.Now)
        {
            throw new SecurityTokenExpiredException();
        }
        
        DateTimeOffset expirationDate = GetExpirationDate(_configuration.Lifetime.RefreshToken, tokenEntity.IsExtendable);
        await _accountsRepository.UpdateRefreshTokenAsync(tokenEntity, x => x.UpdateExpirationDate(expirationDate));
        return account;
    }

    public async Task RevokeRefreshTokenAsync(string stringToken) => 
        await RevokeRefreshTokenAsync(new Guid(Convert.FromBase64String(stringToken)));
    
    public async Task RevokeRefreshTokenAsync(Guid token) =>
        await _accountsRepository.DeleteRefreshTokenAsync(token);

    public async Task RevokeAccountRefreshTokensAsync(Account account) =>
        await _accountsRepository.DeleteUserRefreshTokensAsync(account.Id);
    
    #endregion
    
    
    
    #region PRIVATE METHODS

    private long ValidateExpiredAccessTokenAndGetAccountId(string accessToken)
    {
        TokenValidationParameters tokenValidation = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = false,
            ValidIssuer = _configuration.Issuer,
            ValidAudience = _configuration.Audience,
            IssuerSigningKey = CreateSecurityKey(),
            ClockSkew = TimeSpan.FromMinutes(1),
        };
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        tokenHandler.ValidateToken(accessToken, tokenValidation, out SecurityToken validatedToken);
        JwtSecurityToken? jwtSecurityToken = validatedToken as JwtSecurityToken;
        if (jwtSecurityToken is null || !jwtSecurityToken.Header.Alg.Equals(_configuration.Algorithm, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }

        Claim? sub = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);
        if (sub is null || !long.TryParse(sub.Value, out long accountId))
        {
            throw new SecurityTokenException("Invalid token");
        }

        return accountId;
    }

    private SymmetricSecurityKey CreateSecurityKey()
    {
        string stringKey = _configuration.Key;
        byte[] encodedKey = Encoding.UTF8.GetBytes(stringKey);
        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(encodedKey);
        return securityKey;
    }

    private DateTimeOffset GetExpirationDate(TokenLifetime tokenConfiguration, bool isExtendable = false)
    {
        int lifetime = isExtendable ? tokenConfiguration.Extended ?? tokenConfiguration.Normal : tokenConfiguration.Normal;
        DateTimeOffset expirationDate = DateTimeOffset.UtcNow.AddMinutes(lifetime);
        return expirationDate;
    }
    
    #endregion
}