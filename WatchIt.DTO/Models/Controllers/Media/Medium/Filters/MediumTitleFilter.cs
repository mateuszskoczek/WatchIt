using System.Text.RegularExpressions;
using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Media.Medium.Filters;

public record MediumTitleFilter<T> : Filter<T> where T : Database.Model.Media.Medium
{
    public MediumTitleFilter(string? titleRegex) : base(x =>
    (
        string.IsNullOrWhiteSpace(titleRegex)
        ||
        (
            !string.IsNullOrWhiteSpace(x.Title)
            &&
            Regex.IsMatch(x.Title, titleRegex, RegexOptions.IgnoreCase)
        )
    )) { }
}