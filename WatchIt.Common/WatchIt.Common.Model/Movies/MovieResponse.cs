using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using WatchIt.Database.Model.Media;

namespace WatchIt.Common.Model.Movies;

public class MovieResponse : Movie
{
    #region PROPERTIES

    [JsonPropertyName("id")]
    public long Id { get; set; }

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
    }

    #endregion
}