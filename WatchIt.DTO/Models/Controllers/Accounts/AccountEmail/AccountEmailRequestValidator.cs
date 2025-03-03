using FluentValidation;
using WatchIt.Database;

namespace WatchIt.DTO.Models.Controllers.Accounts.AccountEmail;

public class AccountEmailRequestValidator : AbstractValidator<AccountEmailRequest>
{
    #region CONSTRUCTORS
    
    public AccountEmailRequestValidator(DatabaseContext database)
    {
        RuleFor(x => x.Email).EmailAddress()
                             .CannotBeIn(database.Accounts, x => x.Email).WithMessage("Email is already used");
    }
    
    #endregion
}