using Ardalis.Result;
using WatchIt.DTO.Models.Controllers.Photos.Photo;
using WatchIt.DTO.Models.Controllers.Photos.PhotoBackground;
using WatchIt.DTO.Query;

namespace WatchIt.WebAPI.BusinessLogic.Photos;

public interface IPhotosBusinessLogic
{
    Task<Result<PhotoResponse>> GetPhoto(Guid photoId);
    Task<Result<IEnumerable<PhotoResponse>>> GetPhotos(PhotoFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery);
    Task<Result<PhotoResponse>> GetPhotoBackground();
    Task<Result<PhotoResponse>> PostPhoto(PhotoRequest body);
    Task<Result<PhotoResponse>> PutPhoto(Guid photoId, PhotoRequest body);
    Task<Result> DeletePhoto(Guid photoId);

    Task<Result<PhotoBackgroundResponse>> PutPhotoBackground(Guid photoId, PhotoBackgroundRequest body);
    Task<Result> DeletePhotoBackground(Guid photoId);
}