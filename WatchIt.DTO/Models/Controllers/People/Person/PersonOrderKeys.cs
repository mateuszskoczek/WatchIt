using System.Linq.Expressions;

namespace WatchIt.DTO.Models.Controllers.People.Person;

public class PersonOrderKeys
{
    public static readonly Dictionary<string, Expression<Func<Database.Model.People.Person, object?>>> Base = new Dictionary<string, Expression<Func<Database.Model.People.Person, object?>>>
    {
        { "id", x => x.Id },
        { "name", x => x.Name },
        { "full_name", x => x.FullName },
        { "description", x => x.Description },
        { "birth_date", x => x.BirthDate },
        { "death_date", x => x.BirthDate },
        { "gender", x => x.Gender != null ? x.Gender.Name : null },
        { "rating.average", x => x.Roles.SelectMany(y => y.Ratings).Any() ? x.Roles.SelectMany(y => y.Ratings).Average(y => y.Rating) : 0 },
        { "rating.count", x => x.Roles.SelectMany(y => y.Ratings).Count() },
        { "view_count.last_24_hours", x => x.ViewCounts.Where(y => y.Date >= DateOnly.FromDateTime(DateTime.Now.AddDays(-1))).Sum(y => y.ViewCount) },
        { "view_count.last_week", x => x.ViewCounts.Where(y => y.Date >= DateOnly.FromDateTime(DateTime.Now.AddDays(-7))).Sum(y => y.ViewCount) },
        { "view_count.last_month", x => x.ViewCounts.Where(y => y.Date >= DateOnly.FromDateTime(DateTime.Now.AddMonths(-1))).Sum(y => y.ViewCount) },
        { "view_count.last_year", x => x.ViewCounts.Where(y => y.Date >= DateOnly.FromDateTime(DateTime.Now.AddYears(-1))).Sum(y => y.ViewCount) }
    };
    
    public static Dictionary<string, Expression<Func<Database.Model.People.Person, object?>>> UserRated(long accountId) => new Dictionary<string, Expression<Func<Database.Model.People.Person, object?>>>
    {
        { "rating_user.average", x => x.Roles.SelectMany(y => y.Ratings).Where(y => y.AccountId == accountId).Average(y => y.Rating) },
        { "rating_user.count", x => x.Roles.SelectMany(y => y.Ratings).Count(y => y.AccountId == accountId) },
        { "rating_user.last_date", x => x.Roles.SelectMany(y => y.Ratings).Where(y => y.AccountId == accountId).Max(y => y.Date) },
    }; 
}