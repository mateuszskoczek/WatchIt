using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace WatchIt.Common.Model.Media;

public class MediaResponse : Media
{
    #region PROPERTIES

    public long Id { get; set; }
    
    public MediaType Type { get; set; }

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
    }

    #endregion
}