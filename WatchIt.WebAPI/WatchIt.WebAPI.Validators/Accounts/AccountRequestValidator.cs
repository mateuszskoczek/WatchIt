using FluentValidation;
using WatchIt.Common.Model.Accounts;
using WatchIt.Database;

namespace WatchIt.WebAPI.Validators.Accounts;

public class AccountRequestValidator : AbstractValidator<AccountRequest>
{
    public AccountRequestValidator(DatabaseContext database)
    {
        RuleFor(x => x.Username).NotEmpty()
                                .MaximumLength(50);
        RuleFor(x => x.Email).EmailAddress()
                             .MaximumLength(320);
        RuleFor(x => x.Description).MaximumLength(1000);
        When(x => x.GenderId.HasValue, () =>
        {
            RuleFor(x => x.GenderId!.Value).MustBeIn(database.Genders.Select(x => x.Id));
        });
    }
}