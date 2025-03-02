using FluentValidation;
using WatchIt.Database;

namespace WatchIt.DTO.Models.Controllers.Accounts.Account;

public class AccountRequestValidator : AbstractValidator<AccountRequest>
{
    public AccountRequestValidator(DatabaseContext database)
    {
        Include(new PasswordEditRequestValidator());
        RuleFor(x => x.Username).MinimumLength(5)
                                .MaximumLength(50)
                                .CannotBeIn(database.Accounts, x => x.Username).WithMessage("Username was already used");
        RuleFor(x => x.Email).EmailAddress()
                             .CannotBeIn(database.Accounts, x => x.Email).WithMessage("Email was already used");
    }
}