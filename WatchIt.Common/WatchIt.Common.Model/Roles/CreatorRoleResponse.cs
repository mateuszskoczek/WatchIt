using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using WatchIt.Common.Query;

namespace WatchIt.Common.Model.Roles;

public class CreatorRoleResponse : CreatorRole, IQueryOrderable<CreatorRoleResponse>
{
    #region PROPERTIES

    [JsonIgnore]
    public static IDictionary<string, Func<CreatorRoleResponse, IComparable>> OrderableProperties { get; } = new Dictionary<string, Func<CreatorRoleResponse, IComparable>>
    {
        { "type_id", item => item.TypeId },
    };

    
    [JsonPropertyName("id")]
    public required Guid Id { get; set; }

    #endregion
    
    
    
    #region CONSTRUCTORS

    [JsonConstructor]
    public CreatorRoleResponse() {}
    
    [SetsRequiredMembers]
    public CreatorRoleResponse(Database.Model.Person.PersonCreatorRole data)
    {
        Id = data.Id;
        MediaId = data.MediaId;
        PersonId = data.PersonId;
        TypeId = data.PersonCreatorRoleTypeId;
    }

    #endregion
}