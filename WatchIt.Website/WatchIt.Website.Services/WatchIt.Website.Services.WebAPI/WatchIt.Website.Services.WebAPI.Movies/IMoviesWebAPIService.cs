using WatchIt.Common.Model.Movies;

namespace WatchIt.Website.Services.WebAPI.Movies;

public interface IMoviesWebAPIService
{
    Task GetAllMovies(MovieQueryParameters? query = null, Action<IEnumerable<MovieResponse>>? successAction = null);
    Task PostMovie(MovieRequest data, Action<MovieResponse>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);
    Task GetMovie(long id, Action<MovieResponse>? successAction = null, Action? notFoundAction = null);
    Task PutMovie(long id, MovieRequest data, Action? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);
    Task DeleteMovie(long id, Action? successAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);
}