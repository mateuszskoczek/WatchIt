using System.Text.RegularExpressions;
using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Photos.Photo.Filters;

public record PhotoMimeTypeFilter : Filter<Database.Model.Photos.Photo>
{
    public PhotoMimeTypeFilter(string? queryRegex) : base(x => 
    (
        string.IsNullOrWhiteSpace(queryRegex)
        ||
        (
            !string.IsNullOrWhiteSpace(x.MimeType)
            &&
            Regex.IsMatch(x.MimeType, queryRegex, RegexOptions.IgnoreCase)
        )
    )) { }
};