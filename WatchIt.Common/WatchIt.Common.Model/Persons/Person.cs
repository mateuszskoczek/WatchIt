using System.Text.Json.Serialization;

namespace WatchIt.Common.Model.Persons;

public class Person
{
    #region PROPERTIES
    
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    
    [JsonPropertyName("full_name")]
    public string? FullName { get; set; }
    
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    
    [JsonPropertyName("birth_date")]
    public DateOnly? BirthDate { get; set; }
    
    [JsonPropertyName("death_date")]
    public DateOnly? DeathDate { get; set; }
    
    #endregion
}