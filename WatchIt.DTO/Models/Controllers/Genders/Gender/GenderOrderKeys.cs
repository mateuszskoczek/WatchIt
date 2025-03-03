using System.Linq.Expressions;

namespace WatchIt.DTO.Models.Controllers.Genders.Gender;

public static class GenderOrderKeys
{
    public static readonly Dictionary<string, Expression<Func<Database.Model.Genders.Gender, object?>>> Base = new Dictionary<string, Expression<Func<Database.Model.Genders.Gender, object?>>>
    {
        { "id", x => x.Id },
        { "name", x => x.Name },
    };
}