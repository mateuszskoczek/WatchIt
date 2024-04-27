using WatchIt.Common.Model.Genres;
using WatchIt.WebAPI.Services.Controllers.Common;

namespace WatchIt.WebAPI.Services.Controllers.Genres;

public interface IGenresControllerService
{
    Task<RequestResult> GetAll(GenreQueryParameters query);
    Task<RequestResult> Get(short id);
    Task<RequestResult> Post(GenreRequest data);
    Task<RequestResult> Put(short id, GenreRequest data);
    Task<RequestResult> Delete(short id);
}