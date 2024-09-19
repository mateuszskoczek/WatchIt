using WatchIt.Common.Model.Media;
using WatchIt.WebAPI.Services.Controllers.Common;

namespace WatchIt.WebAPI.Services.Controllers.Media;

public interface IMediaControllerService
{
    Task<RequestResult> GetMedia(long mediaId);
    Task<RequestResult> GetGenres(long mediaId);
    Task<RequestResult> PostGenre(long mediaId, short genreId);
    Task<RequestResult> DeleteGenre(long mediaId, short genreId);
    Task<RequestResult> GetPhoto(Guid id);
    Task<RequestResult> GetPhotos(MediaPhotoQueryParameters query);
    Task<RequestResult> GetRandomBackgroundPhoto();
    Task<RequestResult> GetMediaRandomBackgroundPhoto(long id);
    Task<RequestResult> GetPoster(long id);
    Task<RequestResult> PutPoster(long id, MediaPosterRequest data);
    Task<RequestResult> DeletePoster(long id);
    Task<RequestResult> PostPhoto(MediaPhotoRequest data);
    Task<RequestResult> PutPhoto(Guid photoId, MediaPhotoRequest data);
    Task<RequestResult> DeletePhoto(Guid photoId);
}