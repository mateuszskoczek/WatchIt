using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WatchIt.Shared.Models.Accounts.Authenticate
{
    public class AuthenticateRequest
    {
        #region PROPERTIES

        [JsonPropertyName("username_or_email")]
        public string UsernameOrEmail { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("remember_me")]
        public bool RememberMe { get; set; }

        #endregion
    }
}
