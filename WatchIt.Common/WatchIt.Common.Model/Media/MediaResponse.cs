using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using WatchIt.Common.Model.Rating;
using WatchIt.Common.Query;

namespace WatchIt.Common.Model.Media;

public class MediaResponse : Media, IQueryOrderable<MediaResponse>
{
    #region PROPERTIES

    [JsonIgnore]
    public static IDictionary<string, Func<MediaResponse, IComparable>> OrderableProperties { get; } = new Dictionary<string, Func<MediaResponse, IComparable>>
    {
        { "id", x => x.Id },
        { "title", x => x.Title },
        { "original_title", x => x.OriginalTitle },
        { "description", x => x.Description },
        { "release_date", x => x.ReleaseDate },
        { "length", x => x.Length },
        { "rating.average", x => x.Rating.Average },
        { "rating.count", x => x.Rating.Count }
    };
    
    
    [JsonPropertyName("id")]
    public long Id { get; set; }
    
    [JsonPropertyName("type")]
    public MediaType Type { get; set; }
    
    [JsonPropertyName("rating")]
    public RatingResponse Rating { get; set; }

    #endregion



    #region CONSTRUCTORS

    [JsonConstructor]
    public MediaResponse() {}
    
    [SetsRequiredMembers]
    public MediaResponse(Database.Model.Media.Media media, MediaType mediaType)
    {
        Id = media.Id;
        Title = media.Title;
        OriginalTitle = media.OriginalTitle;
        Description = media.Description;
        ReleaseDate = media.ReleaseDate;
        Length = media.Length;
        Type = mediaType;
        Rating = new RatingResponse(media.RatingMedia);
    }

    #endregion
}