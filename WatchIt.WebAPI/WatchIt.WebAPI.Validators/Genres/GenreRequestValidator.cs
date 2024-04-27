using FluentValidation;
using WatchIt.Common.Model.Genres;
using WatchIt.Database;

namespace WatchIt.WebAPI.Validators.Genres;

public class GenreRequestValidator : AbstractValidator<GenreRequest>
{
    public GenreRequestValidator()
    {
        RuleFor(x => x.Name).MaximumLength(100);
        When(x => !string.IsNullOrWhiteSpace(x.Description), () => RuleFor(x => x.Description).MaximumLength(1000));
    }
}