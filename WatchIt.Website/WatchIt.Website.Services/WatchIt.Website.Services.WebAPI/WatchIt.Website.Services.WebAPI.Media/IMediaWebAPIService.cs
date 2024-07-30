using WatchIt.Common.Model.Genres;
using WatchIt.Common.Model.Media;

namespace WatchIt.Website.Services.WebAPI.Media;

public interface IMediaWebAPIService
{
    Task GetGenres(long mediaId, Action<IEnumerable<GenreResponse>>? successAction = null, Action? notFoundAction = null);
    Task PostGenre(long mediaId, long genreId, Action? successAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null, Action? notFoundAction = null);
    Task GetPhotoRandomBackground(Action<MediaPhotoResponse>? successAction = null, Action? notFoundAction = null);
}