using WatchIt.Common.Model.Media;
using WatchIt.WebAPI.Services.Controllers.Common;

namespace WatchIt.WebAPI.Services.Controllers.Media;

public interface IMediaControllerService
{
    Task<RequestResult> GetMedia(long mediaId);
    
    Task<RequestResult> GetMediaGenres(long mediaId);
    Task<RequestResult> PostMediaGenre(long mediaId, short genreId);
    Task<RequestResult> DeleteMediaGenre(long mediaId, short genreId);

    Task<RequestResult> GetMediaRating(long mediaId);
    Task<RequestResult> GetMediaRatingByUser(long mediaId, long userId);
    Task<RequestResult> PutMediaRating(long mediaId, MediaRatingRequest data);
    Task<RequestResult> DeleteMediaRating(long mediaId);
    
    Task<RequestResult> GetMediaPoster(long id);
    Task<RequestResult> PutMediaPoster(long id, MediaPosterRequest data);
    Task<RequestResult> DeleteMediaPoster(long id);
    
    Task<RequestResult> GetPhoto(Guid id);
    Task<RequestResult> GetPhotos(MediaPhotoQueryParameters query);
    Task<RequestResult> GetRandomBackgroundPhoto();
    Task<RequestResult> GetMediaRandomBackgroundPhoto(long id);
    Task<RequestResult> PostPhoto(MediaPhotoRequest data);
    Task<RequestResult> PutPhoto(Guid photoId, MediaPhotoRequest data);
    Task<RequestResult> DeletePhoto(Guid photoId);
}