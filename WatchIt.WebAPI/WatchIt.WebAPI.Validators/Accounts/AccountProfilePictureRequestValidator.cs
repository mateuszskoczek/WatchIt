using FluentValidation;
using WatchIt.Common.Model.Accounts;

namespace WatchIt.WebAPI.Validators.Accounts;

public class AccountProfilePictureRequestValidator : AbstractValidator<AccountProfilePictureRequest>
{
    public AccountProfilePictureRequestValidator()
    {
        RuleFor(x => x.Image).NotEmpty();
        RuleFor(x => x.MimeType).Matches(@"\w+/.+").WithMessage("Incorrect mimetype");
    }
}