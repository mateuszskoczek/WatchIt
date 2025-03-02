using WatchIt.Database.Model;

namespace WatchIt.DTO.Models.Generics.ViewCount;

public static class ViewCountMappers
{
    #region PUBLIC METHODS

    public static ViewCountResponse ToResponse(this IEnumerable<IViewCountEntity> viewCounts)
    {
        IEnumerable<IViewCountEntity> viewCountEntities = viewCounts.ToList();
        return new ViewCountResponse
        {
            Last24Hours = viewCountEntities.CountFrom(DateTime.Now.AddDays(-1)),
            LastWeek = viewCountEntities.CountFrom(DateTime.Now.AddDays(-7)),
            LastMonth = viewCountEntities.CountFrom(DateTime.Now.AddMonths(-1)),
            LastYear = viewCountEntities.CountFrom(DateTime.Now.AddYears(-1)),
        };
    }

    #endregion
    
    
    
    #region PRIVATE METHODS
    
    private static long CountFrom(this IEnumerable<IViewCountEntity> viewCounts, DateTime date) => viewCounts.Where(x => x.Date >= DateOnly.FromDateTime(date)).Sum(x => x.ViewCount);
    
    #endregion
}