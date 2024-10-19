using FluentValidation;
using WatchIt.Common.Model.Rating;

namespace WatchIt.WebAPI.Validators.Rating;

public class RatingRequestValidator : AbstractValidator<RatingRequest>
{
    public RatingRequestValidator()
    {
        RuleFor(x => x.Rating).InclusiveBetween((short)1, (short)10);
    }
}