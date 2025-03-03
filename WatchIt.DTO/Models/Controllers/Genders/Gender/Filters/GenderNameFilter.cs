using System.Text.RegularExpressions;
using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Genders.Gender.Filters;

public record GenderNameFilter : Filter<Database.Model.Genders.Gender>
{
    public GenderNameFilter(string? nameRegex) : base(x => 
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