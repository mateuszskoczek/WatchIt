using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchIt.WebAPI.Services.Utility.Configuration.Models
{
    public class RefreshToken
    {
        public int Lifetime { get; set; }
        public int ExtendedLifetime { get; set; }
    }
}
