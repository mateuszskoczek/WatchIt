using System.Text.RegularExpressions;
using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Media.Medium.Filters;

public record MediumDescriptionFilter<T> : Filter<T> where T : Database.Model.Media.Medium
{
    public MediumDescriptionFilter(string? descriptionRegex) : base(x =>
    (
        string.IsNullOrWhiteSpace(descriptionRegex)
        ||
        (
            !string.IsNullOrWhiteSpace(x.Description)
            &&
            Regex.IsMatch(x.Description, descriptionRegex, RegexOptions.IgnoreCase)
        )
    )) { }
}