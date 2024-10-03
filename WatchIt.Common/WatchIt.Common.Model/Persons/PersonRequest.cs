using System.Text.Json.Serialization;

namespace WatchIt.Common.Model.Persons;

public class PersonRequest : Person
{
    #region PROPERTIES
    
    [JsonPropertyName("gender_id")]
    public short? GenderId { get; set; }
    
    #endregion
    
    
    
    #region PUBLIC METHODS

    public Database.Model.Person.Person CreatePerson() => new Database.Model.Person.Person
    {
        Name = Name,
        FullName = FullName,
        Description = Description,
        BirthDate = BirthDate,
        DeathDate = DeathDate,
        GenderId = GenderId,
    };

    public void UpdatePerson(Database.Model.Person.Person person)
    {
        person.Name = Name;
        person.FullName = FullName;
        person.Description = Description;
        person.BirthDate = BirthDate;
        person.DeathDate = DeathDate;
        person.GenderId = GenderId;
    }

    #endregion
}