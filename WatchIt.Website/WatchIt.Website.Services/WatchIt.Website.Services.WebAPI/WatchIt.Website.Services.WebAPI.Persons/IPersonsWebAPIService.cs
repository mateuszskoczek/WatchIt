using WatchIt.Common.Model.Persons;
using WatchIt.Common.Model.Roles;

namespace WatchIt.Website.Services.WebAPI.Persons;

public interface IPersonsWebAPIService
{
    Task GetAllPersons(PersonQueryParameters? query = null, Action<IEnumerable<PersonResponse>>? successAction = null);
    Task GetPerson(long id, Action<PersonResponse>? successAction = null, Action? notFoundAction = null);
    Task PostPerson(PersonRequest data, Action<PersonResponse>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);
    Task PutPerson(long id, PersonRequest data, Action? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);
    Task DeletePerson(long id, Action? successAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);
    
    Task GetPersonsViewRank(int? first = null, int? days = null, Action<IEnumerable<PersonResponse>>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null);
    
    Task GetPersonPhoto(long id, Action<PersonPhotoResponse>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? notFoundAction = null);
    Task PutPersonPhoto(long id, PersonPhotoRequest data, Action<PersonPhotoResponse>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);
    Task DeletePersonPhoto(long id, Action? successAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);

    Task GetPersonAllActorRoles(long id, ActorRolePersonQueryParameters? query = null, Action<IEnumerable<ActorRoleResponse>>? successAction = null);
    Task PostPersonActorRole(long id, IActorRolePersonRequest data, Action<ActorRoleResponse>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);
    Task GetPersonAllCreatorRoles(long id, CreatorRolePersonQueryParameters? query = null, Action<IEnumerable<CreatorRoleResponse>>? successAction = null);
    Task PostPersonCreatorRole(long id, ICreatorRolePersonRequest data, Action<CreatorRoleResponse>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);
}