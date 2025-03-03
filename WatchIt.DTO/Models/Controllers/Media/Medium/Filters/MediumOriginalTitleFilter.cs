using System.Text.RegularExpressions;
using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Media.Medium.Filters;

public record MediumOriginalTitleFilter<T> : Filter<T> where T : Database.Model.Media.Medium
{
    public MediumOriginalTitleFilter(string? originalTitleRegex) : base(x =>
    (
        string.IsNullOrWhiteSpace(originalTitleRegex)
        ||
        (
            !string.IsNullOrWhiteSpace(x.OriginalTitle)
            &&
            Regex.IsMatch(x.OriginalTitle, originalTitleRegex, RegexOptions.IgnoreCase)
        )
    )) { }
}