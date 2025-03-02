using System.Text.RegularExpressions;
using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Genres.Genre.Filters;

public record GenreNameFilter : Filter<Database.Model.Genres.Genre>
{
    public GenreNameFilter(string? nameRegex) : base(x => 
    (
        string.IsNullOrWhiteSpace(nameRegex)
        ||
        (
            !string.IsNullOrWhiteSpace(x.Name)
            &&
            Regex.IsMatch(x.Name, nameRegex, RegexOptions.IgnoreCase)
        )
    )) { }
};