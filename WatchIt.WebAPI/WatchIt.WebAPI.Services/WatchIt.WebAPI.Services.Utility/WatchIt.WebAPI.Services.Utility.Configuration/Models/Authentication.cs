using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchIt.WebAPI.Services.Utility.Configuration.Models
{
    public class Authentication
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public RefreshToken RefreshToken { get; set; }
        public AccessToken AccessToken { get; set; }
    }
}
