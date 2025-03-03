using FluentValidation;

namespace WatchIt.DTO.Models.Controllers.Genders.Gender;

public class GenderRequestValidator : AbstractValidator<GenderRequest>
{
    #region CONSTRUCTORS
    
    public GenderRequestValidator()
    {
        RuleFor(x => x.Name).MaximumLength(100);
    }
    
    #endregion
}