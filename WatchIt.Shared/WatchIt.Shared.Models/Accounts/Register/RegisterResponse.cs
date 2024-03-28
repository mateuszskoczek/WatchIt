using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WatchIt.Database.Model.Account;

namespace WatchIt.Shared.Models.Accounts.Register
{
    public class RegisterResponse
    {
        #region PROPERTIES

        [JsonPropertyName("id")]
        public required long Id { get; init; }

        [JsonPropertyName("username")]
        public required string Username { get; init; }

        [JsonPropertyName("email")]
        public required string Email { get; init; }

        [JsonPropertyName("creation_date")]
        public required DateTime CreationDate { get; init; }

        #endregion



        #region CONVERTION

        public static implicit operator RegisterResponse(Account account) => new RegisterResponse
        {
            Id = account.Id,
            Username = account.Username,
            Email = account.Email,
            CreationDate = account.CreationDate
        };

        #endregion
    }
}
