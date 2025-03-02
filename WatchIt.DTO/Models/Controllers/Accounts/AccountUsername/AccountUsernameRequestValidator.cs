using FluentValidation;
using WatchIt.Database;

namespace WatchIt.DTO.Models.Controllers.Accounts.AccountUsername;

public class AccountUsernameRequestValidator : AbstractValidator<AccountUsernameRequest>
{
    #region CONSTRUCTORS
    
    public AccountUsernameRequestValidator(DatabaseContext database)
    {
        RuleFor(x => x.Username).MinimumLength(5)
                                .MaximumLength(50)
                                .CannotBeIn(database.Accounts, x => x.Username).WithMessage("Username is already used");
    }
    
    #endregion
}