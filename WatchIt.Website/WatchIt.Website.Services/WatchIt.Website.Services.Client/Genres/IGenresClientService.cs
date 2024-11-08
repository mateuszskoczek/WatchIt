using WatchIt.Common.Model.Genres;
using WatchIt.Common.Model.Media;

namespace WatchIt.Website.Services.Client.Genres;

public interface IGenresClientService
{
    Task GetGenres(GenreQueryParameters? query = null, Action<IEnumerable<GenreResponse>>? successAction = null);
    Task GetGenre(long id, Action<GenreResponse>? successAction = null, Action? notFoundAction = null);
    Task PostGenre(GenreRequest data, Action<GenreResponse>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);
    Task DeleteGenre(long id, Action? successAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);
    Task GetGenreMedia(short id, MediaQueryParameters? query = null, Action<IEnumerable<MediaResponse>>? successAction = null, Action notFoundAction = null);
}