using System.Diagnostics.CodeAnalysis;
using WatchIt.Database.Model.Media;

namespace WatchIt.Common.Model.Series;

public class SeriesRequest : Series
{
    #region CONSTRUCTORS

    [SetsRequiredMembers]
    public SeriesRequest(SeriesResponse initData)
    {
        Title = initData.Title;
        OriginalTitle = initData.OriginalTitle;
        Description = initData.Description;
        ReleaseDate = initData.ReleaseDate;
        Length = initData.Length;
        HasEnded = initData.HasEnded;
    }
    
    public SeriesRequest() {}
    
    #endregion
    
    
    
    #region PUBLIC METHODS

    public Database.Model.Media.Media CreateMedia() => new Database.Model.Media.Media
    {
        Title = Title,
        OriginalTitle = OriginalTitle,
        Description = Description,
        ReleaseDate = ReleaseDate,
        Length = Length,
    };

    public MediaSeries CreateMediaSeries(long id) => new MediaSeries 
    { 
        Id = id,
        HasEnded = HasEnded,
    };

    public void UpdateMedia(Database.Model.Media.Media media)
    {
        media.Title = Title;
        media.OriginalTitle = OriginalTitle;
        media.Description = Description;
        media.ReleaseDate = ReleaseDate;
        media.Length = Length;
    }

    public void UpdateMediaSeries(MediaSeries mediaSeries)
    {
        mediaSeries.HasEnded = HasEnded;
    }

    #endregion
}