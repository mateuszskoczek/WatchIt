using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using WatchIt.Database.Model.Person;

namespace WatchIt.Common.Model.Persons;

public class PersonPhotoResponse : PersonPhoto
{
    #region PROPERTIES
    
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    [JsonPropertyName("upload_date")]
    public DateTime UploadDate { get; set; }
    
    #endregion
    
    
    
    #region CONSTRUCTORS
    
    [JsonConstructor]
    public PersonPhotoResponse() {}
    
    [SetsRequiredMembers]
    public PersonPhotoResponse(PersonPhotoImage personPhotoImage)
    {
        Id = personPhotoImage.Id;
        Image = personPhotoImage.Image;
        MimeType = personPhotoImage.MimeType;
        UploadDate = personPhotoImage.UploadDate;
    }
    
    #endregion
}