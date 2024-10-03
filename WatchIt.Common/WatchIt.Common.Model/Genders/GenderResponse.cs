using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace WatchIt.Common.Model.Genders;

public class GenderResponse : Gender
{
    #region PROPERTIES

    [JsonPropertyName("id")]
    public required short? Id { get; set; }

    #endregion
    
    
    
    #region CONSTRUCTORS

    [SetsRequiredMembers]
    public GenderResponse(Database.Model.Common.Gender gender)
    {
        Id = gender.Id;
        Name = gender.Name;
    }
    
    #endregion
}