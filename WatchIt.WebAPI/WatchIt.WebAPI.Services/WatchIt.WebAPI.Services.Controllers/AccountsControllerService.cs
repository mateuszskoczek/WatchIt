using Microsoft.EntityFrameworkCore;
using SimpleToolkit.Extensions;
using System.Security.Cryptography;
using System.Text;
using WatchIt.Database;
using WatchIt.Database.Model.Account;
using WatchIt.Shared.Models;
using WatchIt.Shared.Models.Accounts.Authenticate;
using WatchIt.Shared.Models.Accounts.Register;
using WatchIt.WebAPI.Services.Utility.JWT;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WatchIt.WebAPI.Services.Controllers
{
    public interface IAccountsControllerService
    {
        Task<RequestResult<RegisterResponse>> Register(RegisterRequest data);
        Task<RequestResult<AuthenticateResponse>> Authenticate(AuthenticateRequest data);
        Task<RequestResult<AuthenticateResponse>> AuthenticateRefresh();
    }

    public class AccountsControllerService(IJWTService jwtService, DatabaseContext database) : IAccountsControllerService
    {
        #region PUBLIC METHODS

        public async Task<RequestResult<RegisterResponse>> Register(RegisterRequest data)
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
                RightSalt = rightSalt
            };
            await database.Accounts.AddAsync(account);
            await database.SaveChangesAsync();

            return RequestResult.Created<RegisterResponse>($"accounts/{account.Id}", account);
        }

        public async Task<RequestResult<AuthenticateResponse>> Authenticate(AuthenticateRequest data)
        {
            Account? account = await database.Accounts.FirstOrDefaultAsync(x => string.Equals(x.Email, data.UsernameOrEmail) || string.Equals(x.Username, data.UsernameOrEmail));
            if (account is null)
            {
                return RequestResult.Unauthorized<AuthenticateResponse>("User does not exists");
            }

            byte[] hash = ComputeHash(data.Password, account.LeftSalt, account.RightSalt);
            if (!Enumerable.SequenceEqual(hash, account.Password))
            {
                return RequestResult.Unauthorized<AuthenticateResponse>("Incorrect password");
            }

            Task<string> refreshTokenTask = jwtService.CreateRefreshToken(account, true);
            Task<string> accessTokenTask = jwtService.CreateAccessToken(account);
            await Task.WhenAll(refreshTokenTask, accessTokenTask);

            AuthenticateResponse response = new AuthenticateResponse
            {
                AccessToken = accessTokenTask.Result,
                RefreshToken = refreshTokenTask.Result,
            };

            return RequestResult.Ok(response);
        }

        public async Task<RequestResult<AuthenticateResponse>> AuthenticateRefresh()
        {

        }

        #endregion



        #region PRIVATE METHODS

        protected byte[] ComputeHash(string password, string leftSalt, string rightSalt) => SHA512.Create().ComputeHash(Encoding.UTF8.GetBytes($"{leftSalt}{password}{rightSalt}"));

        #endregion
    }
}
