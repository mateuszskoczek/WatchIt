using FluentValidation;
using WatchIt.Database;

namespace WatchIt.DTO.Models.Controllers.Accounts.AccountBackgroundPicture;

public class AccountBackgroundPictureRequestValidator : AbstractValidator<AccountBackgroundPictureRequest>
{
    #region CONSTRUCTORS
    
    public AccountBackgroundPictureRequestValidator(DatabaseContext database)
    {
        RuleFor(x => x.Id).MustBeIn(database.PhotoBackgrounds, x => x.Id).WithMessage("Background does not exists");
    }
    #endregion
}