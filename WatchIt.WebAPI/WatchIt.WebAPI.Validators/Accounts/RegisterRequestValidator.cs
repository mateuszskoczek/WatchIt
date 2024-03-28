using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchIt.Database;
using WatchIt.Shared.Models.Accounts.Register;

namespace WatchIt.WebAPI.Validators.Accounts
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        #region CONSTRUCTOR

        public RegisterRequestValidator(DatabaseContext database)
        {
            RuleFor(x => x.Username).MinimumLength(5)
                                    .MaximumLength(50)
                                    .CannotBeIn(database.Accounts, x => x.Username).WithMessage("Username was already used");
            RuleFor(x => x.Email).EmailAddress()
                                 .CannotBeIn(database.Accounts, x => x.Email).WithMessage("Email was already used");
            RuleFor(x => x.Password).MinimumLength(8)
                                    .Must(x => x.Any(c => Char.IsUpper(c))).WithMessage("Password must contain at least one uppercase letter.")
                                    .Must(x => x.Any(c => Char.IsLower(c))).WithMessage("Password must contain at least one lowercase letter.")
                                    .Must(x => x.Any(c => Char.IsDigit(c))).WithMessage("Password must contain at least one digit.");
        }

        #endregion
    }
}
