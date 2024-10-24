using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using WatchIt.Common.Query;

namespace WatchIt.Common.Model.Roles;

public class RoleTypeResponse : RoleType, IQueryOrderable<RoleTypeResponse>
{
    #region PROPERTIES

    [JsonIgnore]
    public static IDictionary<string, Func<RoleTypeResponse, IComparable>> OrderableProperties { get; } = new Dictionary<string, Func<RoleTypeResponse, IComparable>>
    {
        { "name", item => item.Name }
    };

    
    [JsonPropertyName("id")]
    public required short Id { get; set; }

    #endregion
    
    
    
    #region CONSTRUCTORS
    
    [JsonConstructor]
    public RoleTypeResponse() { }

    [SetsRequiredMembers]
    public RoleTypeResponse(Database.Model.Person.PersonCreatorRoleType creatorRoleType)
    {
        Id = creatorRoleType.Id;
        Name = creatorRoleType.Name;
    }

    [SetsRequiredMembers]
    public RoleTypeResponse(Database.Model.Person.PersonActorRoleType actorRoleType)
    {
        Id = actorRoleType.Id;
        Name = actorRoleType.Name;
    }
    
    #endregion
}