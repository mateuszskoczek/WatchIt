using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using WatchIt.Common.Model.Genres;
using WatchIt.Common.Model.Rating;
using WatchIt.Common.Query;
using WatchIt.Database.Model.Media;

namespace WatchIt.Common.Model.Movies;

public class MovieResponse : Movie, IQueryOrderable<MovieResponse>
{
    #region PROPERTIES

    [JsonIgnore]
    public static IDictionary<string, Func<MovieResponse, IComparable>> OrderableProperties { get; } = new Dictionary<string, Func<MovieResponse, IComparable>>
    {
        { "id", x => x.Id },
        { "title", x => x.Title },
        { "original_title", x => x.OriginalTitle },
        { "description", x => x.Description },
        { "release_date", x => x.ReleaseDate },
        { "length", x => x.Length },
        { "budget", x => x.Budget },
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
    public MovieResponse() {}
    
    [SetsRequiredMembers]
    public MovieResponse(MediaMovie mediaMovie)
    {
        Id = mediaMovie.Media.Id;
        Title = mediaMovie.Media.Title;
        OriginalTitle = mediaMovie.Media.OriginalTitle;
        Description = mediaMovie.Media.Description;
        ReleaseDate = mediaMovie.Media.ReleaseDate;
        Length = mediaMovie.Media.Length;
        Budget = mediaMovie.Budget;
        Rating = RatingResponseBuilder.Initialize()
                                      .Add(mediaMovie.Media.RatingMedia, x => x.Rating)
                                      .Build();
        Genres = mediaMovie.Media.Genres.Select(x => new GenreResponse(x)).ToList();
    }

    #endregion
}