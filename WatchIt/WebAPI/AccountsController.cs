using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WatchIt.WebAPI
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountsController : ControllerBase
    {
        [HttpPost]
        [Route("create-account")]
        public async Task CreateAccount()
        {
        }
    }
}
