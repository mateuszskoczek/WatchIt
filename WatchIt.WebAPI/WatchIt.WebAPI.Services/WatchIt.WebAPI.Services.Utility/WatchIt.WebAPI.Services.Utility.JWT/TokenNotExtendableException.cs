using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchIt.WebAPI.Services.Utility.JWT
{
    public class TokenNotExtendableException : Exception
    {
        public TokenNotExtendableException() : base() { }
    }
}
