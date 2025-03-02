using System.Linq.Expressions;
using WatchIt.Database.Model.Media;

namespace WatchIt.DTO.Models.Controllers.Media.Medium;

public static class MediumOrderKeys
{
    public static Dictionary<string, Expression<Func<T, object?>>> Base<T>() where T : Database.Model.Media.Medium => new Dictionary<string, Expression<Func<T, object?>>>
    {
        { "id", x => x.Id },
        { "title", x => x.Title },
        { "original_title", x => x.OriginalTitle },
        { "description", x => x.Description },
        { "release_date", x => x.ReleaseDate },
        { "rating.average", x => x.Ratings.Any() ? x.Ratings.Average(y => y.Rating) : 0 },
        { "rating.count", x => x.Ratings.Count() },
        { "view_count.last_24_hours", x => x.ViewCounts.Where(y => y.Date.ToDateTime(new TimeOnly(23, 59)) >= DateTime.Now.AddDays(-1)).Sum(y => y.ViewCount) },
        { "view_count.last_week", x => x.ViewCounts.Where(y => y.Date.ToDateTime(new TimeOnly(23, 59)) >= DateTime.Now.AddDays(-7)).Sum(y => y.ViewCount) },
        { "view_count.last_month", x => x.ViewCounts.Where(y => y.Date.ToDateTime(new TimeOnly(23, 59)) >= DateTime.Now.AddMonths(-1)).Sum(y => y.ViewCount) },
        { "view_count.last_year", x => x.ViewCounts.Where(y => y.Date.ToDateTime(new TimeOnly(23, 59)) >= DateTime.Now.AddYears(-1)).Sum(y => y.ViewCount) }
    };
    
    public static readonly Dictionary<string, Expression<Func<Database.Model.Media.Medium, object?>>> Medium = new Dictionary<string, Expression<Func<Database.Model.Media.Medium, object?>>>
    {
        { "type", x => x.Type },
    };
    
    public static readonly Dictionary<string, Expression<Func<MediumMovie, object?>>> MediumMovie = new Dictionary<string, Expression<Func<MediumMovie, object?>>>
    {
        { "budget", x => x.Budget },
    };
    
    public static readonly Dictionary<string, Expression<Func<MediumSeries, object?>>> MediumSeries = new Dictionary<string, Expression<Func<MediumSeries, object?>>>
    {
        { "has_ended", x => x.HasEnded },
    };
    
    public static Dictionary<string, Expression<Func<T, object?>>> MediumUserRated<T>(long accountId) where T : Database.Model.Media.Medium => new Dictionary<string, Expression<Func<T, object?>>>
    {
        { "rating_user.rating", x => x.Ratings.FirstOrDefault(x => x.AccountId == accountId) != null ? x.Ratings.FirstOrDefault(x => x.AccountId == accountId).Rating : null },
        { "rating_user.date", x => x.Ratings.FirstOrDefault(x => x.AccountId == accountId) != null ? x.Ratings.FirstOrDefault(x => x.AccountId == accountId).Date : null },
    };
}