using WatchIt.Common.Model.Persons;
using WatchIt.Common.Model.Roles;
using WatchIt.WebAPI.Services.Controllers.Common;

namespace WatchIt.WebAPI.Services.Controllers.Persons;

public interface IPersonsControllerService
{
    Task<RequestResult> GetAllPersons(PersonQueryParameters query);
    Task<RequestResult> GetPerson(long id);
    Task<RequestResult> PostPerson(PersonRequest data);
    Task<RequestResult> PutPerson(long id, PersonRequest data);
    Task<RequestResult> DeletePerson(long id);
    
    Task<RequestResult> GetPersonsViewRank(int first, int days);

    Task<RequestResult> GetPersonPhoto(long id);
    Task<RequestResult> PutPersonPhoto(long id, PersonPhotoRequest data);
    Task<RequestResult> DeletePersonPhoto(long id);

    Task<RequestResult> GetPersonAllActorRoles(long personId, ActorRolePersonQueryParameters queryParameters);
    Task<RequestResult> PostPersonActorRole(long personId, ActorRolePersonRequest data);
    Task<RequestResult> GetPersonAllCreatorRoles(long personId, CreatorRolePersonQueryParameters queryParameters);
    Task<RequestResult> PostPersonCreatorRole(long personId, CreatorRolePersonRequest data);
}