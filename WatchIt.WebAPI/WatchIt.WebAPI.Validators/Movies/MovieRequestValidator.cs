using FluentValidation;
using WatchIt.Common.Model.Movies;

namespace WatchIt.WebAPI.Validators.Movies;

public class MovieRequestValidator : AbstractValidator<MovieRequest>
{
    public MovieRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(250);
        RuleFor(x => x.OriginalTitle).MaximumLength(250);
        RuleFor(x => x.Description).MaximumLength(1000);
    }
}