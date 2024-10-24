using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using WatchIt.Database.Model.Person;

namespace WatchIt.Common.Model.Persons;

public class PersonPhotoRequest : PersonPhoto
{
    #region CONSTRUCTORS
    
    [JsonConstructor]
    public PersonPhotoRequest() {}

    [SetsRequiredMembers]
    public PersonPhotoRequest(Picture image)
    {
        Image = image.Image;
        MimeType = image.MimeType;
    }
    
    #endregion
    
    
    
    #region PUBLIC METHODS
    
    public PersonPhotoImage CreatePersonPhotoImage() => new PersonPhotoImage
    {
        Image = Image,
        MimeType = MimeType,
    };
    
    public void UpdatePersonPhotoImage(PersonPhotoImage item)
    {
        item.Image = Image;
        item.MimeType = MimeType;
        item.UploadDate = DateTime.UtcNow;
    }
    
    #endregion
}