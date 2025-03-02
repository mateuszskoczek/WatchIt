using System.Linq.Expressions;

namespace WatchIt.DTO.Models.Controllers.Genres.Genre;

public static class GenreOrderKeys
{
    public static readonly Dictionary<string, Expression<Func<Database.Model.Genres.Genre, object?>>> Base = new Dictionary<string, Expression<Func<Database.Model.Genres.Genre, object?>>>
    {
        { "id", x => x.Id },
        { "name", x => x.Name },
    };
}