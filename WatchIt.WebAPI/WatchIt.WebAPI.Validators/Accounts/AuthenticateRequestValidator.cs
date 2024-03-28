using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WatchIt.Database;
using WatchIt.Shared.Models.Accounts.Authenticate;

namespace WatchIt.WebAPI.Validators.Accounts
{
    public class AuthenticateRequestValidator : AbstractValidator<AuthenticateRequest>
    {
        #region CONSTRUCTOR

        public AuthenticateRequestValidator(DatabaseContext database)
        {
            RuleFor(x => x.UsernameOrEmail).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }

        #endregion
    }
}
