using WatchIt.Common.Model.Photos;

namespace WatchIt.Website.Services.WebAPI.Photos;

public interface IPhotosWebAPIService
{
    Task GetPhotoRandomBackground(Action<PhotoResponse>? successAction = null, Action? notFoundAction = null);
    Task DeletePhoto(Guid id, Action<PhotoResponse>? successAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null, Action? notFoundAction = null);
    Task PutPhotoBackgroundData(Guid id, PhotoBackgroundDataRequest data, Action<PhotoResponse>? successAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null, Action? notFoundAction = null);
    Task DeletePhotoBackgroundData(Guid id, Action<PhotoResponse>? successAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null, Action? notFoundAction = null);
}