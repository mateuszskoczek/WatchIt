using Microsoft.EntityFrameworkCore;
using WatchIt.Common.Model.Persons;
using WatchIt.Database;
using WatchIt.WebAPI.Services.Controllers.Common;
using WatchIt.WebAPI.Services.Utility.User;
using Person = WatchIt.Database.Model.Person.Person;

namespace WatchIt.WebAPI.Services.Controllers.Persons;

public class PersonsControllerService : IPersonsControllerService
{
    #region SERVICES

    private readonly DatabaseContext _database;
    private readonly IUserService _userService;
    
    #endregion
    
    
    
    #region CONSTRUCTORS

    public PersonsControllerService(DatabaseContext database, IUserService userService)
    {
        _database = database;
        _userService = userService;
    }
    
    #endregion
    
    
    
    #region PUBLIC METHODS
    
    #region Main

    public async Task<RequestResult> GetAllPersons(PersonQueryParameters query)
    {
        IEnumerable<Person> rawData = await _database.Persons.ToListAsync();
        IEnumerable<PersonResponse> data = rawData.Select(x => new PersonResponse(x));
        data = query.PrepareData(data);
        return RequestResult.Ok(data);
    }
    
    public async Task<RequestResult> GetPerson(long id)
    {
        Person? item = await _database.Persons.FirstOrDefaultAsync(x => x.Id == id);
        if (item is null)
        {
            return RequestResult.NotFound();
        }

        PersonResponse data = new PersonResponse(item);
        return RequestResult.Ok(data);
    }
    
    public async Task<RequestResult> PostPerson(PersonRequest data)
    {
        UserValidator validator = _userService.GetValidator().MustBeAdmin();
        if (!validator.IsValid)
        {
            return RequestResult.Forbidden();
        }

        Person personItem = data.CreatePerson();
        await _database.Persons.AddAsync(personItem);
        await _database.SaveChangesAsync();
        
        return RequestResult.Created($"persons/{personItem.Id}", new PersonResponse(personItem));
    }
    
    public async Task<RequestResult> PutPerson(long id, PersonRequest data)
    {
        UserValidator validator = _userService.GetValidator().MustBeAdmin();
        if (!validator.IsValid)
        {
            return RequestResult.Forbidden();
        }
        
        Person? item = await _database.Persons.FirstOrDefaultAsync(x => x.Id == id);
        if (item is null)
        {
            return RequestResult.NotFound();
        }
        
        data.UpdatePerson(item);
        
        await _database.SaveChangesAsync();
        
        return RequestResult.Ok();
    }
    
    public async Task<RequestResult> DeletePerson(long id)
    {
        UserValidator validator = _userService.GetValidator().MustBeAdmin();
        if (!validator.IsValid)
        {
            return RequestResult.Forbidden();
        }
        
        Person? item = await _database.Persons.FirstOrDefaultAsync(x => x.Id == id);
        if (item is null)
        {
            return RequestResult.NotFound();
        }

        _database.PersonCreatorRoles.AttachRange(item.PersonCreatorRoles);
        _database.PersonCreatorRoles.RemoveRange(item.PersonCreatorRoles);
        _database.PersonActorRoles.AttachRange(item.PersonActorRoles);
        _database.PersonActorRoles.RemoveRange(item.PersonActorRoles);
        _database.ViewCountsPerson.AttachRange(item.ViewCountsPerson);
        _database.ViewCountsPerson.RemoveRange(item.ViewCountsPerson);
        _database.Persons.Attach(item);
        _database.Persons.Remove(item);
        await _database.SaveChangesAsync();
        
        return RequestResult.NoContent();
    }
    
    #endregion

    #region View count

    public async Task<RequestResult> GetPersonsViewRank(int first, int days)
    {
        if (first < 1 || days < 1)
        {
            return RequestResult.BadRequest();
        }
        
        DateOnly startDate = DateOnly.FromDateTime(DateTime.Now).AddDays(-days);
        IEnumerable<Person> rawData = await _database.Persons.OrderByDescending(x => x.ViewCountsPerson.Where(y => y.Date >= startDate)
                                                                                                       .Sum(y => y.ViewCount))
                                                             .ThenBy(x => x.Id)
                                                             .Take(first)
                                                             .ToListAsync();
        
        IEnumerable<PersonResponse> data = rawData.Select(x => new PersonResponse(x));
        
        return RequestResult.Ok(data);
    }

    #endregion
    
    #endregion
}