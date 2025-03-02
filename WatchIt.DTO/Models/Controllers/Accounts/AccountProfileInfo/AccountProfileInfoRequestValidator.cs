using FluentValidation;
using WatchIt.Database;

namespace WatchIt.DTO.Models.Controllers.Accounts.AccountProfileInfo;

public class AccountProfileInfoRequestValidator : AbstractValidator<AccountProfileInfoRequest>
{
    public AccountProfileInfoRequestValidator(DatabaseContext database)
    {
        RuleFor(x => x.Description).MaximumLength(1000);
        When(x => x.GenderId.HasValue, () =>
        {
            RuleFor(x => x.GenderId!.Value).MustBeIn(database.Genders.Select(x => x.Id));
        });
    }
}