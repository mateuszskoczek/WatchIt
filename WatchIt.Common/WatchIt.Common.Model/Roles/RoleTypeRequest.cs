namespace WatchIt.Common.Model.Roles;

public class RoleTypeRequest : RoleType
{
    #region PUBLIC METHODS

    public Database.Model.Person.PersonActorRoleType CreateActorRoleType() => new Database.Model.Person.PersonActorRoleType()
    {
        Name = Name
    };
    
    public Database.Model.Person.PersonCreatorRoleType CreateCreatorRoleType() => new Database.Model.Person.PersonCreatorRoleType()
    {
        Name = Name
    };

    #endregion
}