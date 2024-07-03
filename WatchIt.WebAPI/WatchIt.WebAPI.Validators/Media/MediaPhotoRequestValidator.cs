using FluentValidation;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using WatchIt.Common.Model.Media;
using WatchIt.Database;

namespace WatchIt.WebAPI.Validators.Media;

public class MediaPhotoRequestValidator : AbstractValidator<MediaPhotoRequest>
{
    public MediaPhotoRequestValidator(DatabaseContext database)
    {
        RuleFor(x => x.MediaId).MustBeIn(database.Media, x => x.Id).WithMessage("Media does not exists");
        RuleFor(x => x.Image).NotEmpty();
        RuleFor(x => x.MimeType).Matches(@"\w+/.+").WithMessage("Incorrect mimetype");
        When(x => x.Background is not null, () =>
        {
            RuleFor(x => x.Background!.FirstGradientColor).Must(x => x.Length == 3).WithMessage("First gradient color has to be 3 byte long");
            RuleFor(x => x.Background!.SecondGradientColor).Must(x => x.Length == 3).WithMessage("Second gradient color has to be 3 byte long");
        });
    }
}