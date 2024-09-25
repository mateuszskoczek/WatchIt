using FluentValidation;
using WatchIt.Common.Model.Photos;

namespace WatchIt.WebAPI.Validators.Photos;

public class PhotoBackgroundDataRequestValidator : AbstractValidator<PhotoBackgroundDataRequest>
{
    public PhotoBackgroundDataRequestValidator()
    {
        RuleFor(x => x).SetValidator(new PhotoBackgroundDataValidator());
    }
}