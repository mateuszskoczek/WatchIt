using WatchIt.Database.Model.People;
using WatchIt.DTO.Models.Controllers.People.Person.Query;
using WatchIt.DTO.Query;

namespace WatchIt.WebAPI.Repositories.People;

public interface IPeopleRepository : IRepository<Person>
{
    Task<bool> ExistsAsync(long id);
    Task<Person?> GetAsync(long id, Func<IQueryable<Person>, IQueryable<Person>>? additionalIncludes = null);
    Task<IEnumerable<Person>> GetAllAsync(PersonFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, Func<IQueryable<Person>, IQueryable<Person>>? additionalIncludes = null);
    Task<bool> UpdateAsync(long id, Action<Person> updateFunc, Func<IQueryable<Person>, IQueryable<Person>>? additionalIncludes = null);
    Task DeleteAsync(long id);

    Task<PersonViewCount> UpdateOrAddPersonViewCountAsync(long personId, DateOnly date, Func<PersonViewCount> addFunc, Action<PersonViewCount> updateFunc);

    Task<PersonPicture?> GetPersonPictureAsync(long id, Func<IQueryable<PersonPicture>, IQueryable<PersonPicture>>? additionalIncludes = null);
    Task<PersonPicture> UpdateOrAddPersonPictureAsync(long id, Func<PersonPicture> addFunc, Action<PersonPicture> updateFunc);
    Task DeletePersonPictureAsync(long id);

    Task<IEnumerable<Person>> GetAllRatedByAccountAsync(long accountId, PersonFilterQuery filterQuery, PersonUserRatedFilterQuery userRatedFilterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, Func<IQueryable<Person>, IQueryable<Person>>? additionalIncludes = null);
}