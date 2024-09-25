using FluentValidation;
using WatchIt.Common.Model.Photos;

namespace WatchIt.WebAPI.Validators.Photos;

public class PhotoBackgroundDataValidator : AbstractValidator<PhotoBackgroundData>
{
    public PhotoBackgroundDataValidator()
    {
        RuleFor(x => x.FirstGradientColor).Must(x => x.Length == 3).WithMessage("First gradient color has to be 3 byte long");
        RuleFor(x => x.SecondGradientColor).Must(x => x.Length == 3).WithMessage("Second gradient color has to be 3 byte long");
    }
}