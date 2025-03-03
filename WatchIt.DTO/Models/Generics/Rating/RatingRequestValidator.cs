using FluentValidation;

namespace WatchIt.DTO.Models.Generics.Rating;

public class RatingRequestValidator : AbstractValidator<RatingRequest>
{
    #region CONSTRUCTORS
    
    public RatingRequestValidator()
    {
        RuleFor(x => x.Rating).InclusiveBetween((byte)1, (byte)10);
    }
    
    #endregion
}