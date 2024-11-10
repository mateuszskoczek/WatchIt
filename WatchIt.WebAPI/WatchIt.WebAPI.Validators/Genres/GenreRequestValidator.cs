using FluentValidation;
using WatchIt.Common.Model.Genres;
using WatchIt.Database;

namespace WatchIt.WebAPI.Validators.Genres;

public class GenreRequestValidator : AbstractValidator<GenreRequest>
{
    public GenreRequestValidator()
    {
        RuleFor(x => x.Name).MaximumLength(100);
    }
}