using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using WatchIt.Common.Model.Genres;
using WatchIt.Common.Model.Rating;
using WatchIt.Common.Query;
using WatchIt.Database.Model.Media;
using WatchIt.Database.Model.Rating;

namespace WatchIt.Common.Model.Movies;

public class MovieRatedResponse : MovieResponse, IQueryOrderable<MovieRatedResponse>
{
    #region PROPERTIES
    
    [JsonIgnore]
    public static IDictionary<string, Func<MovieRatedResponse, IComparable>> OrderableProperties { get; } = new Dictionary<string, Func<MovieRatedResponse, IComparable>>
    {
        { "id", x => x.Id },
        { "title", x => x.Title },
        { "original_title", x => x.OriginalTitle },
        { "description", x => x.Description },
        { "release_date", x => x.ReleaseDate },
        { "length", x => x.Length },
        { "budget", x => x.Budget },
        { "rating.average", x => x.Rating.Average },
        { "rating.count", x => x.Rating.Count },
        { "user_rating", x => x.UserRating },
        { "user_rating_date", x => x.UserRatingDate }
    };
    
    [JsonPropertyName("user_rating")]
    public short UserRating { get; set; }
    
    [JsonPropertyName("user_rating_date")]
    public DateTime UserRatingDate { get; set; }
    
    #endregion



    #region CONSTRUCTORS

    [JsonConstructor]
    public MovieRatedResponse() { }

    [SetsRequiredMembers]
    public MovieRatedResponse(MediaMovie mediaMovie, RatingMedia response)
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
        UserRating = response.Rating;
        UserRatingDate = response.Date;
    }

    #endregion
}