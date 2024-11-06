using WatchIt.Common.Model.Genders;

namespace WatchIt.Website.Services.Client.Genders;

public interface IGendersClientService
{
    Task GetAllGenders(GenderQueryParameters? query = null, Action<IEnumerable<GenderResponse>>? successAction = null);
    Task GetGender(long id, Action<GenderResponse>? successAction = null, Action? notFoundAction = null);
    Task PostGender(GenderRequest data, Action<GenderResponse>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);
    Task DeleteGender(long id, Action? successAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);
}