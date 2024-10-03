using WatchIt.Common.Model.Persons;

namespace WatchIt.Website.Services.WebAPI.Persons;

public interface IPersonsWebAPIService
{
    Task GetAllPersons(PersonQueryParameters? query = null, Action<IEnumerable<PersonResponse>>? successAction = null);
    Task GetPerson(long id, Action<PersonResponse>? successAction = null, Action? notFoundAction = null);
    Task PostPerson(PersonRequest data, Action<PersonResponse>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);
    Task PutPerson(long id, PersonRequest data, Action? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);
    Task DeletePerson(long id, Action? successAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);
    Task GetPersonsViewRank(int? first = null, int? days = null, Action<IEnumerable<PersonResponse>>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null);
}