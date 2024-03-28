using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WatchIt.Shared.Models.Accounts.Authenticate
{
    public class AuthenticateResponse
    {
        #region PROPERTIES

        [JsonPropertyName("access_token")]
        public required string AccessToken { get; init; }

        [JsonPropertyName("refresh_token")]
        public required string RefreshToken { get; init; }

        #endregion
    }
}
