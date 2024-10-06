using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using WatchIt.Common.Query;

namespace WatchIt.Common.Model.Roles;

public class ActorRoleResponse : ActorRole, IQueryOrderable<ActorRoleResponse>
{
    #region PROPERTIES

    [JsonIgnore]
    public static IDictionary<string, Func<ActorRoleResponse, IComparable>> OrderableProperties { get; } = new Dictionary<string, Func<ActorRoleResponse, IComparable>>
    {
        { "name", item => item.Name },
        { "type_id", item => item.TypeId },
    };

    
    [JsonPropertyName("id")]
    public required Guid Id { get; set; }

    #endregion
    
    
    
    #region CONSTRUCTORS

    [JsonConstructor]
    public ActorRoleResponse() {}
    
    [SetsRequiredMembers]
    public ActorRoleResponse(Database.Model.Person.PersonActorRole data)
    {
        Id = data.Id;
        MediaId = data.MediaId;
        PersonId = data.PersonId;
        TypeId = data.PersonActorRoleTypeId;
        Name = data.RoleName;
    }

    #endregion
}