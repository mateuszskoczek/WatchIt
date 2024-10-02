using Microsoft.EntityFrameworkCore;
using WatchIt.Common.Model.Persons;
using WatchIt.Database;
using WatchIt.WebAPI.Services.Controllers.Common;
using WatchIt.WebAPI.Services.Utility.User;
using Person = WatchIt.Database.Model.Person.Person;

namespace WatchIt.WebAPI.Services.Controllers.Persons;

public class PersonsControllerService
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
    
    public async Task<RequestResult> GetMovie(long id)
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
    
    #endregion
    
    #endregion
}