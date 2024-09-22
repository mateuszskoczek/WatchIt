using FluentValidation;
using WatchIt.Common.Model.Series;

namespace WatchIt.WebAPI.Validators.Movies;

public class SeriesRequestValidator : AbstractValidator<SeriesRequest>
{
    public SeriesRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(250);
        RuleFor(x => x.OriginalTitle).MaximumLength(250);
        RuleFor(x => x.Description).MaximumLength(1000);
    }
}