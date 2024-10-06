using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using WatchIt.Common.Query;

namespace WatchIt.Common.Model.Roles;

public class RoleResponse : Role, IQueryOrderable<RoleResponse>
{
    #region PROPERTIES

    [JsonIgnore]
    public static IDictionary<string, Func<RoleResponse, IComparable>> OrderableProperties { get; } = new Dictionary<string, Func<RoleResponse, IComparable>>
    {
        { "name", item => item.Name }
    };

    
    [JsonPropertyName("id")]
    public required short? Id { get; set; }

    #endregion
    
    
    
    #region CONSTRUCTORS
    
    [JsonConstructor]
    public RoleResponse() { }

    [SetsRequiredMembers]
    public RoleResponse(Database.Model.Person.PersonCreatorRoleType creatorRoleType)
    {
        Id = creatorRoleType.Id;
        Name = creatorRoleType.Name;
    }

    [SetsRequiredMembers]
    public RoleResponse(Database.Model.Person.PersonActorRoleType actorRoleType)
    {
        Id = actorRoleType.Id;
        Name = actorRoleType.Name;
    }
    
    #endregion
}