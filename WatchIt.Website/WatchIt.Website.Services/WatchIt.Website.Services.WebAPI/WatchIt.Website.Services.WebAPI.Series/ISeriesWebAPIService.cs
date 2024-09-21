using WatchIt.Common.Model.Series;

namespace WatchIt.Website.Services.WebAPI.Series;

public interface ISeriesWebAPIService
{
    Task GetAllSeries(SeriesQueryParameters? query = null, Action<IEnumerable<SeriesResponse>>? successAction = null);
    Task GetSeries(long id, Action<SeriesResponse>? successAction = null, Action? notFoundAction = null);
    Task PostSeries(SeriesRequest data, Action<SeriesResponse>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);
    Task PutSeries(long id, SeriesRequest data, Action? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);
    Task DeleteSeries(long id, Action? successAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);

    Task GetSeriesViewRank(int? first = null, int? days = null, Action<IEnumerable<SeriesResponse>>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null);
}