using WatchIt.Common.Model.Media;
using WatchIt.Common.Model.Photos;
using WatchIt.Common.Model.Rating;
using WatchIt.Common.Model.Roles;
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
    Task<RequestResult> PutMediaRating(long mediaId, RatingRequest data);
    Task<RequestResult> DeleteMediaRating(long mediaId);

    Task<RequestResult> PostMediaView(long mediaId);
    
    Task<RequestResult> GetMediaPoster(long mediaId);
    Task<RequestResult> PutMediaPoster(long mediaId, MediaPosterRequest data);
    Task<RequestResult> DeleteMediaPoster(long mediaId);

    Task<RequestResult> GetMediaPhotos(long mediaId, PhotoQueryParameters queryParameters);
    Task<RequestResult> GetMediaPhotoRandomBackground(long mediaId);
    Task<RequestResult> PostMediaPhoto(long mediaId, MediaPhotoRequest data);

    Task<RequestResult> GetMediaAllActorRoles(long mediaId, ActorRoleMediaQueryParameters queryParameters);
    Task<RequestResult> PostMediaActorRole(long mediaId, IActorRoleMediaRequest data);
    Task<RequestResult> GetMediaAllCreatorRoles(long mediaId, CreatorRoleMediaQueryParameters queryParameters);
    Task<RequestResult> PostMediaCreatorRole(long mediaId, ICreatorRoleMediaRequest data);
}