using Refit;
using WatchIt.DTO.Models.Controllers.Photos.Photo;
using WatchIt.DTO.Models.Controllers.Photos.PhotoBackground;
using WatchIt.DTO.Query;

namespace WatchIt.Website.Clients;

public interface IPhotosClient
{
    #region Main

    [Get("/{id}")]
    Task<IApiResponse<PhotoResponse>> GetPhoto(Guid id);

    [Get("/")]
    Task<IApiResponse<IEnumerable<PhotoResponse>>> GetPhotos([Query] PhotoFilterQuery? filterQuery = null, [Query] OrderQuery? orderQuery = null, [Query] PagingQuery? pagingQuery = null);

    [Post("/")]
    Task<IApiResponse<PhotoResponse>> PostPhoto([Authorize]string token, [Body] PhotoRequest body);

    [Put("/{id}")]
    Task<IApiResponse> PutPhoto([Authorize]string token, Guid id, [Body] PhotoRequest body);

    [Delete("/{id}")]
    Task<IApiResponse> DeletePhoto([Authorize]string token, Guid id);

    #endregion

    #region Background

    [Get("/background")]
    Task<IApiResponse<PhotoResponse>> GetPhotoBackground();

    [Put("/{photo_id}/background")]
    Task<IApiResponse<PhotoBackgroundResponse>> PutPhotoBackground([Authorize]string token, [AliasAs("photo_id")] Guid photoId, [Body] PhotoBackgroundRequest body);

    [Delete("/{photo_id}/background")]
    Task<IApiResponse> DeletePhotoBackground([Authorize]string token, [AliasAs("photo_id")] Guid photoId);

    #endregion
}