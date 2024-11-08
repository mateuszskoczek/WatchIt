using WatchIt.Common.Model.Genres;
using WatchIt.Common.Model.Media;
using WatchIt.WebAPI.Services.Controllers.Common;

namespace WatchIt.WebAPI.Services.Controllers.Genres;

public interface IGenresControllerService
{
    Task<RequestResult> GetGenres(GenreQueryParameters query);
    Task<RequestResult> GetGenre(short id);
    Task<RequestResult> PostGenre(GenreRequest data);
    Task<RequestResult> PutGenre(short id, GenreRequest data);
    Task<RequestResult> DeleteGenre(short id);
    Task<RequestResult> GetGenreMedia(short id, MediaQueryParameters query);
}