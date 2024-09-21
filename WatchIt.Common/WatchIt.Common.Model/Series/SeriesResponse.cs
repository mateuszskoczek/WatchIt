using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using WatchIt.Database.Model.Media;

namespace WatchIt.Common.Model.Series;

public class SeriesResponse : Series
{
    #region PROPERTIES

    [JsonPropertyName("id")]
    public long Id { get; set; }

    #endregion
    
    
    
    #region CONSTRUCTORS

    [JsonConstructor]
    public SeriesResponse() {}
    
    [SetsRequiredMembers]
    public SeriesResponse(MediaSeries mediaSeries)
    {
        Id = mediaSeries.Media.Id;
        Title = mediaSeries.Media.Title;
        OriginalTitle = mediaSeries.Media.OriginalTitle;
        Description = mediaSeries.Media.Description;
        ReleaseDate = mediaSeries.Media.ReleaseDate;
        Length = mediaSeries.Media.Length;
        HasEnded = mediaSeries.HasEnded;
    }

    #endregion
}