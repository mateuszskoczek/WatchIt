using WatchIt.Common.Model.Genres;
using WatchIt.Common.Model.Media;

namespace WatchIt.Website.Services.WebAPI.Media;

public interface IMediaWebAPIService
{
    Task GetGenres(long mediaId, Action<IEnumerable<GenreResponse>> successAction, Action notFoundAction);
    Task PostGenre(long mediaId, long genreId, Action successAction, Action unauthorizedAction, Action forbiddenAction, Action notFoundAction);
    Task GetPhotoRandomBackground(Action<MediaPhotoResponse> successAction, Action notFoundAction);
}