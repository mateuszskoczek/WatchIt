using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using WatchIt.Common.Model.Genres;
using WatchIt.Common.Model.Rating;
using WatchIt.Common.Query;
using WatchIt.Database.Model.Media;

namespace WatchIt.Common.Model.Series;

public class SeriesResponse : Series, IQueryOrderable<SeriesResponse>
{
    #region PROPERTIES

    [JsonIgnore]
    public static IDictionary<string, Func<SeriesResponse, IComparable>> OrderableProperties { get; } = new Dictionary<string, Func<SeriesResponse, IComparable>>
    {
        { "id", x => x.Id },
        { "title", x => x.Title },
        { "original_title", x => x.OriginalTitle },
        { "description", x => x.Description },
        { "release_date", x => x.ReleaseDate },
        { "length", x => x.Length },
        { "has_ended", x => x.HasEnded },
        { "rating.average", x => x.Rating.Average },
        { "rating.count", x => x.Rating.Count }
    };
    

    [JsonPropertyName("id")]
    public long Id { get; set; }
    
    [JsonPropertyName("rating")]
    public RatingResponse Rating { get; set; }
    
    [JsonPropertyName("genres")]
    public IEnumerable<GenreResponse> Genres { get; set; }

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
        Rating = RatingResponseBuilder.Initialize()
                                      .Add(mediaSeries.Media.RatingMedia, x => x.Rating)
                                      .Build();
        Genres = mediaSeries.Media.Genres.Select(x => new GenreResponse(x)).ToList();
    }

    #endregion
}