using System.Text.Json.Serialization;
using WatchIt.Database.Model.Person;

namespace WatchIt.Common.Model.Roles;

public class CreatorRoleUniversalRequest : CreatorRoleRequest, ICreatorRolePersonRequest, ICreatorRoleMediaRequest
{
    #region PROPERTIES
    
    [JsonPropertyName("person_id")]
    public long PersonId { get; set; }
    
    [JsonPropertyName("media_id")]
    public long MediaId { get; set; }
    
    #endregion
    
    
    
    #region CONSTRUCTORS
    
    public CreatorRoleUniversalRequest() { }

    public CreatorRoleUniversalRequest(CreatorRoleResponse data)
    {
        MediaId = data.MediaId;
        PersonId = data.PersonId;
        TypeId = data.TypeId;
    }
    
    #endregion



    #region PUBLIC METHODS

    public PersonCreatorRole CreateCreatorRole() => base.CreateCreatorRole(MediaId, PersonId);
    
    public void UpdateCreatorRole(PersonCreatorRole item) => base.UpdateCreatorRole(item, MediaId, PersonId);

    #endregion
}