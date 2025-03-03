using FluentValidation;

namespace WatchIt.DTO.Models.Generics.Image;

public class ImageRequestValidator : AbstractValidator<ImageRequest>
{
    #region CONSTRUCTORS
    
    public ImageRequestValidator()
    {
        RuleFor(x => x.Image).NotEmpty();
        RuleFor(x => x.MimeType).Matches(@"\w+/.+").WithMessage("Incorrect mimetype");
    }
    
    #endregion
}