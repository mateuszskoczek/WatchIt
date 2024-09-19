using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace WatchIt.Common.Model.Genres;

public class GenreResponse : Genre
{
    #region PROPERTIES

    [JsonPropertyName("id")]
    public long Id { get; set; }

    #endregion
    
    
    
    #region CONSTRUCTORS

    [JsonConstructor]
    public GenreResponse() {}
    
    [SetsRequiredMembers]
    public GenreResponse(Database.Model.Common.Genre genre)
    {
        Id = genre.Id;
        Name = genre.Name;
        Description = genre.Description;
    }
    
    #endregion
}