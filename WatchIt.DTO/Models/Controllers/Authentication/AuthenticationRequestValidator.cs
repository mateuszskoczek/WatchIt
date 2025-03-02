using FluentValidation;

namespace WatchIt.DTO.Models.Controllers.Authentication;

public class AuthenticationRequestValidator : AbstractValidator<AuthenticationRequest>
{
    public AuthenticationRequestValidator()
    {
        RuleFor(x => x.UsernameOrEmail).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}