using WatchIt.Common.Model.Movies;

namespace WatchIt.Website.Services.WebAPI.Movies;

public interface IMoviesWebAPIService
{
    Task GetAll(MovieQueryParameters? query = null, Action<IEnumerable<MovieResponse>>? successAction = null);
    Task Post(MovieRequest data, Action<MovieResponse>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);
    Task Get(long id, Action<MovieResponse>? successAction = null, Action? notFoundAction = null);
    Task Put(long id, MovieRequest data, Action? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);
    Task Delete(long id, Action? successAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);
}