using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using WatchIt.Common.Model.Genres;
using WatchIt.Common.Model.Rating;
using WatchIt.Common.Query;
using WatchIt.Database.Model.Media;
using WatchIt.Database.Model.Rating;

namespace WatchIt.Common.Model.Series;

public class SeriesRatedResponse : SeriesResponse, IQueryOrderable<SeriesRatedResponse>
{
    #region PROPERTIES
    
    [JsonIgnore]
    public static IDictionary<string, Func<SeriesRatedResponse, IComparable>> OrderableProperties { get; } = new Dictionary<string, Func<SeriesRatedResponse, IComparable>>
    {
        { "id", x => x.Id },
        { "title", x => x.Title },
        { "original_title", x => x.OriginalTitle },
        { "description", x => x.Description },
        { "release_date", x => x.ReleaseDate },
        { "length", x => x.Length },
        { "has_ended", x => x.HasEnded },
        { "rating.average", x => x.Rating.Average },
        { "rating.count", x => x.Rating.Count },
        { "user_rating", x => x.UserRating }
    };
    
    [JsonPropertyName("user_rating")]
    public short UserRating { get; set; }
    
    #endregion



    #region CONSTRUCTORS

    [JsonConstructor]
    public SeriesRatedResponse() { }

    [SetsRequiredMembers]
    public SeriesRatedResponse(MediaSeries mediaSeries, RatingMedia response)
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
        UserRating = response.Rating;
    }

    #endregion
}