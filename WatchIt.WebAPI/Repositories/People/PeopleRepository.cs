using Microsoft.EntityFrameworkCore;
using WatchIt.Database;
using WatchIt.Database.Model.People;
using WatchIt.Database.Model.Roles;
using WatchIt.DTO.Models.Controllers.People.Person;
using WatchIt.DTO.Models.Controllers.People.Person.Query;
using WatchIt.DTO.Query;

namespace WatchIt.WebAPI.Repositories.People;

public class PeopleRepository : Repository<Person>, IPeopleRepository
{
    #region CONSTRUCTORS
    
    public PeopleRepository(DatabaseContext database) : base(database) {}
    
    #endregion
    
    
    
    #region PUBLIC METHODS
    
    #region Main
    
    public async Task<bool> ExistsAsync(long id) =>
        await DefaultSet.AnyAsync(x => x.Id == id);

    public async Task<Person?> GetAsync(long id, Func<IQueryable<Person>, IQueryable<Person>>? additionalIncludes = null) =>
        await DefaultSet.Include(additionalIncludes)
                        .FirstOrDefaultAsync(x => x.Id == id);
    
    public async Task<IEnumerable<Person>> GetAllAsync(PersonFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, Func<IQueryable<Person>, IQueryable<Person>>? additionalIncludes = null) =>
        await DefaultSet.ApplyFilter(filterQuery)
                        .ApplyOrder(orderQuery, PersonOrderKeys.Base)
                        .ApplyPaging(pagingQuery)
                        .Include(additionalIncludes)
                        .ToListAsync();

    public async Task<bool> UpdateAsync(long id, Action<Person> updateFunc, Func<IQueryable<Person>, IQueryable<Person>>? additionalIncludes = null)
    {
        Person? person = await GetAsync(id, additionalIncludes);
        if (person is null)
        {
            return false;
        }
        await UpdateAsync(person, updateFunc);
        return true;
    }
    
    public async Task DeleteAsync(long id) =>
        await DeleteAsync<Person>(new Person { Id = id });
    
    #endregion
    
    #region View count
    
    public async Task<PersonViewCount> UpdateOrAddPersonViewCountAsync(long personId, DateOnly date, Func<PersonViewCount> addFunc, Action<PersonViewCount> updateFunc) =>
        await UpdateOrAddAsync(await Database.PersonViewCounts
                                             .FirstOrDefaultAsync(x => x.PersonId == personId && x.Date == date), addFunc, updateFunc);
    
    #endregion
    
    #region Picture
    
    public async Task<PersonPicture?> GetPersonPictureAsync(long id, Func<IQueryable<PersonPicture>, IQueryable<PersonPicture>>? additionalIncludes = null) =>
        await Database.PersonPictures
                      .Include(additionalIncludes)
                      .FirstOrDefaultAsync(x => x.PersonId == id);
    
    public async Task<PersonPicture> UpdateOrAddPersonPictureAsync(long id, Func<PersonPicture> addFunc, Action<PersonPicture> updateFunc) =>
        await UpdateOrAddAsync(await GetPersonPictureAsync(id), addFunc, updateFunc);

    public async Task DeletePersonPictureAsync(long id) =>
        await DeleteAsync(new PersonPicture { PersonId = id });
    
    #endregion

    #region Rating

    public async Task<IEnumerable<Person>> GetAllRatedByAccountAsync(long accountId, PersonFilterQuery filterQuery, PersonUserRatedFilterQuery userRatedFilterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, Func<IQueryable<Person>, IQueryable<Person>>? additionalIncludes = null) =>
        await DefaultSet.Where(x => x.Roles
                                     .SelectMany(y => y.Ratings)
                                     .Any(y => y.AccountId == accountId))
                        .ApplyFilter(filterQuery)
                        .ApplyFilter(userRatedFilterQuery)
                        .ApplyOrder(orderQuery, PersonOrderKeys.Base, PersonOrderKeys.UserRated(accountId))
                        .ApplyPaging(pagingQuery)
                        .Include(additionalIncludes)
                        .ToListAsync();

    #endregion
    
    #endregion
}