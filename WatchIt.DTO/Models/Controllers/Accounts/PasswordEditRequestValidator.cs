using FluentValidation;

namespace WatchIt.DTO.Models.Controllers.Accounts;

public class PasswordEditRequestValidator : AbstractValidator<IPasswordEditRequest>
{
    #region CONSTRUCTORS
    
    public PasswordEditRequestValidator()
    {
        RuleFor(x => x.Password).NotNull()
                                .NotEmpty();
        When(x => x.Password is not null, () =>
        {
            RuleFor(x => x.Password).MinimumLength(8)
                                    .Must(x => x.Any(char.IsUpper)).WithMessage("Password must contain at least one uppercase letter.")
                                    .Must(x => x.Any(char.IsLower)).WithMessage("Password must contain at least one lowercase letter.")
                                    .Must(x => x.Any(char.IsDigit)).WithMessage("Password must contain at least one digit.");
        });
        RuleFor(x => x.PasswordConfirmation).Equal(x => x.Password);
    }
    
    #endregion
}