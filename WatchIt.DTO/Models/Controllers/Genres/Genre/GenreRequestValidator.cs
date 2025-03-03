using FluentValidation;

namespace WatchIt.DTO.Models.Controllers.Genres.Genre;

public class GenreRequestValidator : AbstractValidator<GenreRequest>
{
    #region CONSTRUCTORS
    
    public GenreRequestValidator()
    {
        RuleFor(x => x.Name).MaximumLength(100);
    }
    
    #endregion
}