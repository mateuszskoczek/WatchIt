using FluentValidation;
using WatchIt.Database;

namespace WatchIt.DTO.Models.Controllers.People.Person;

public class PersonRequestValidator : AbstractValidator<PersonRequest>
{
    #region CONSTRUCTORS
    
    public PersonRequestValidator(DatabaseContext database)
    {
        RuleFor(x => x.Name).NotEmpty()
                            .MaximumLength(100);
        RuleFor(x => x.FullName).MaximumLength(200);
        RuleFor(x => x.Description).MaximumLength(1000);
        When(x => x.GenderId.HasValue, () =>
        {
            RuleFor(x => x.GenderId!.Value).MustBeIn(database.Genders.Select(g => g.Id));
        });
    }
    #endregion
}