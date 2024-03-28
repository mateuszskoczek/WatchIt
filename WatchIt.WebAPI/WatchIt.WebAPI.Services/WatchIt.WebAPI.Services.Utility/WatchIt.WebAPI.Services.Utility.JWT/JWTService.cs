using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using WatchIt.Database;
using WatchIt.Database.Model.Account;
using WatchIt.WebAPI.Services.Utility.Configuration;

namespace WatchIt.WebAPI.Services.Utility.JWT
{
    public interface IJWTService
    {
        Task<string> CreateAccessToken(Account account);
        Task<string> CreateRefreshToken(Account account, bool extendable);
        Task<string> ExtendRefreshToken(Account account, Guid id);
    }



    public class JWTService(IConfigurationService configurationService, DatabaseContext database) : IJWTService
    {
        #region PUBLIC METHODS

        public async Task<string> CreateRefreshToken(Account account, bool extendable)
        {
            int expirationMinutes = extendable ? configurationService.Data.Authentication.RefreshToken.ExtendedLifetime : configurationService.Data.Authentication.RefreshToken.Lifetime;
            DateTime expirationDate = DateTime.UtcNow.AddMinutes(expirationMinutes);
            Guid id = Guid.NewGuid();

            AccountRefreshToken refreshToken = new AccountRefreshToken
            {
                Id = id,
                AccountId = account.Id,
                ExpirationDate = expirationDate,
                IsExtendable = extendable
            };
            database.AccountRefreshTokens.Add(refreshToken);
            Task saveTask = database.SaveChangesAsync();

            SecurityTokenDescriptor tokenDescriptor = CreateBaseSecurityTokenDescriptor(account, id, expirationDate);
            tokenDescriptor.Audience = "refresh";
            tokenDescriptor.Subject.AddClaim(new Claim("extend", extendable.ToString()));

            string tokenString = TokenToString(tokenDescriptor);

            await saveTask;

            return tokenString;
        }

        public async Task<string> ExtendRefreshToken(Account account, Guid id)
        {
            AccountRefreshToken? token = account.AccountRefreshTokens.FirstOrDefault(x => x.Id == id);
            if (token is null)
            {
                throw new TokenNotFoundException();
            }
            if (!token.IsExtendable)
            {
                throw new TokenNotExtendableException();
            }

            int expirationMinutes = configurationService.Data.Authentication.RefreshToken.ExtendedLifetime;
            DateTime expirationDate = DateTime.UtcNow.AddMinutes(expirationMinutes);

            token.ExpirationDate = expirationDate;

            Task saveTask = database.SaveChangesAsync();

            SecurityTokenDescriptor tokenDescriptor = CreateBaseSecurityTokenDescriptor(account, id, expirationDate);
            tokenDescriptor.Audience = "refresh";
            tokenDescriptor.Subject.AddClaim(new Claim("extend", bool.TrueString));

            string tokenString = TokenToString(tokenDescriptor);

            await saveTask;

            return tokenString;
        }

        public async Task<string> CreateAccessToken(Account account)
        {
            DateTime lifetime = DateTime.Now.AddMinutes(configurationService.Data.Authentication.AccessToken.Lifetime);
            Guid id = Guid.NewGuid();

            SecurityTokenDescriptor tokenDescriptor = CreateBaseSecurityTokenDescriptor(account, id, lifetime);
            tokenDescriptor.Audience = "access";
            tokenDescriptor.Subject.AddClaim(new Claim("admin", account.IsAdmin.ToString()));

            return TokenToString(tokenDescriptor);
        }

        #endregion



        #region PRIVATE METHODS

        protected SecurityTokenDescriptor CreateBaseSecurityTokenDescriptor(Account account, Guid id, DateTime expirationTime) => new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, account.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, account.Email),
                new Claim(JwtRegisteredClaimNames.UniqueName, account.Username),
                new Claim(JwtRegisteredClaimNames.Exp, expirationTime.ToString()),
            }),
            Expires = expirationTime,
            Issuer = configurationService.Data.Authentication.Issuer,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configurationService.Data.Authentication.Key)), SecurityAlgorithms.HmacSha512)
        };

        protected string TokenToString(SecurityTokenDescriptor tokenDescriptor)
        {
            System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            handler.InboundClaimTypeMap.Clear();

            SecurityToken token = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(token);
        }

        #endregion
    }
}
