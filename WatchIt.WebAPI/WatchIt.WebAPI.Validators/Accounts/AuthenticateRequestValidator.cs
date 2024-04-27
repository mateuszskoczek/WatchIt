using FluentValidation;
using WatchIt.Common.Model.Accounts;

namespace WatchIt.WebAPI.Validators.Accounts;

public class AuthenticateRequestValidator : AbstractValidator<AuthenticateRequest>
{
    public AuthenticateRequestValidator()
    {
        RuleFor(x => x.UsernameOrEmail).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}