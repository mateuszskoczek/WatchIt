using FluentValidation;

namespace WatchIt.DTO.Models.Controllers.Accounts.AccountPassword;

public class AccountPasswordRequestValidator : AbstractValidator<AccountPasswordRequest>
{
    #region CONSTRUCTORS
    
    public AccountPasswordRequestValidator()
    {
        Include(new PasswordEditRequestValidator());
    }
    
    #endregion
}