using WatchIt.Common.Model.Photos;
using WatchIt.WebAPI.Services.Controllers.Common;

namespace WatchIt.WebAPI.Services.Controllers.Photos;

public interface IPhotosControllerService
{
    Task<RequestResult> GetPhotoRandomBackground();
    Task<RequestResult> DeletePhoto(Guid photoId);

    Task<RequestResult> PutPhotoBackgroundData(Guid id, PhotoBackgroundDataRequest data);
    Task<RequestResult> DeletePhotoBackgroundData(Guid id);
}