using WatchIt.Common.Model.Movies;
using WatchIt.WebAPI.Services.Controllers.Common;

namespace WatchIt.WebAPI.Services.Controllers.Movies;

public interface IMoviesControllerService
{
    Task<RequestResult> GetAllMovies(MovieQueryParameters query);
    Task<RequestResult> GetMovie(long id);
    Task<RequestResult> PostMovie(MovieRequest data);
    Task<RequestResult> PutMovie(long id, MovieRequest data);
    Task<RequestResult> DeleteMovie(long id);

    Task<RequestResult> GetMoviesViewRank(int first, int days);
}