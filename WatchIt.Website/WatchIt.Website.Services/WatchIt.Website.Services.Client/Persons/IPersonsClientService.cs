using WatchIt.Common.Model.Persons;
using WatchIt.Common.Model.Rating;
using WatchIt.Common.Model.Roles;

namespace WatchIt.Website.Services.Client.Persons;

public interface IPersonsClientService
{
    Task GetAllPersons(PersonQueryParameters? query = null, Action<IEnumerable<PersonResponse>>? successAction = null);
    Task GetPerson(long id, Action<PersonResponse>? successAction = null, Action? notFoundAction = null);
    Task PostPerson(PersonRequest data, Action<PersonResponse>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);
    Task PutPerson(long id, PersonRequest data, Action? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);
    Task DeletePerson(long id, Action? successAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);
    
    Task GetPersonsViewRank(int? first = null, int? days = null, Action<IEnumerable<PersonResponse>>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null);
    Task PostPersonView(long personId, Action? successAction = null, Action? notFoundAction = null);
    
    Task GetPersonPhoto(long id, Action<PersonPhotoResponse>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? notFoundAction = null);
    Task PutPersonPhoto(long id, PersonPhotoRequest data, Action<PersonPhotoResponse>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);
    Task DeletePersonPhoto(long id, Action? successAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);

    Task GetPersonAllActorRoles(long id, ActorRolePersonQueryParameters? query = null, Action<IEnumerable<ActorRoleResponse>>? successAction = null);
    Task PostPersonActorRole(long id, ActorRolePersonRequest data, Action<ActorRoleResponse>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);
    Task GetPersonAllCreatorRoles(long id, CreatorRolePersonQueryParameters? query = null, Action<IEnumerable<CreatorRoleResponse>>? successAction = null);
    Task PostPersonCreatorRole(long id, CreatorRolePersonRequest data, Action<CreatorRoleResponse>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);

    Task GetPersonGlobalRating(long id, Action<RatingResponse>? successAction = null, Action? notFoundAction = null);
    Task GetPersonUserRating(long id, long userId, Action<RatingResponse>? successAction = null, Action? notFoundAction = null);
}