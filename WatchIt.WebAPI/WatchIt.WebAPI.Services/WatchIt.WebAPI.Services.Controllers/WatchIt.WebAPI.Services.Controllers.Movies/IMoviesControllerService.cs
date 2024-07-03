using WatchIt.Common.Model.Movies;
using WatchIt.WebAPI.Services.Controllers.Common;

namespace WatchIt.WebAPI.Services.Controllers.Movies;

public interface IMoviesControllerService
{
    Task<RequestResult> GetAll(MovieQueryParameters query);
    Task<RequestResult> Get(long id);
    Task<RequestResult> Post(MovieRequest data);
    Task<RequestResult> Put(long id, MovieRequest data);
    Task<RequestResult> Delete(long id);
}