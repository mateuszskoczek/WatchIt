using WatchIt.Common.Model.Series;
using WatchIt.WebAPI.Services.Controllers.Common;

namespace WatchIt.WebAPI.Services.Controllers.Series;

public interface ISeriesControllerService
{
    Task<RequestResult> GetAllSeries(SeriesQueryParameters query);
    Task<RequestResult> GetSeries(long id);
    Task<RequestResult> PostSeries(SeriesRequest data);
    Task<RequestResult> PutSeries(long id, SeriesRequest data);
    Task<RequestResult> DeleteSeries(long id);

    Task<RequestResult> GetSeriesViewRank(int first, int days);
}