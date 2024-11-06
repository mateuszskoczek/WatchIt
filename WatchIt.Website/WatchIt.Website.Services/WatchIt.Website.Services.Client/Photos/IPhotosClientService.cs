using WatchIt.Common.Model.Photos;

namespace WatchIt.Website.Services.Client.Photos;

public interface IPhotosClientService
{
    Task GetPhotoRandomBackground(Action<PhotoResponse>? successAction = null, Action? notFoundAction = null);
    Task DeletePhoto(Guid id, Action? successAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null, Action? notFoundAction = null);
    Task PutPhotoBackgroundData(Guid id, PhotoBackgroundDataRequest data, Action? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null, Action? notFoundAction = null);
    Task DeletePhotoBackgroundData(Guid id, Action? successAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null, Action? notFoundAction = null);
}