using WatchIt.Database.Model.Media;

namespace WatchIt.Database.Model.Media;

public class MediumViewCount : IViewCountEntity
{
    #region PROPERTIES

    public long MediumId { get; set; }
    public DateOnly Date { get; set; }
    public long ViewCount { get; set; }
    public uint Version { get; set; }

    #endregion
    
    
    
    #region NAVIGATION
    
    public virtual Medium Medium { get; set; } = default!;
    
    #endregion
}