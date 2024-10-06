using Microsoft.EntityFrameworkCore;
using WatchIt.Common.Model.Roles;
using WatchIt.Database;
using WatchIt.Database.Model.Person;
using WatchIt.WebAPI.Services.Controllers.Common;
using WatchIt.WebAPI.Services.Utility.User;

namespace WatchIt.WebAPI.Services.Controllers.Roles;

public class RolesControllerService : IRolesControllerService
{
    #region SERVICES

    private readonly DatabaseContext _database;
    private readonly IUserService _userService;
    
    #endregion
    
    
    
    #region CONSTRUCTORS

    public RolesControllerService(DatabaseContext database, IUserService userService)
    {
        _database = database;
        _userService = userService;
    }
    
    #endregion



    #region PUBLIC METHODS
    
    #region Actor
    
    public async Task<RequestResult> GetAllActorRoleTypes(RoleTypeQueryParameters query)
    {
        IEnumerable<PersonActorRoleType> rawData = await _database.PersonActorRoleTypes.ToListAsync();
        IEnumerable<RoleTypeResponse> data = rawData.Select(x => new RoleTypeResponse(x));
        data = query.PrepareData(data);
        return RequestResult.Ok(data);
    }

    public async Task<RequestResult> GetActorRoleType(short id)
    {
        PersonActorRoleType? item = await _database.PersonActorRoleTypes.FirstOrDefaultAsync(x => x.Id == id);
        if (item is null)
        {
            return RequestResult.NotFound();
        }

        RoleTypeResponse data = new RoleTypeResponse(item);
        return RequestResult.Ok(data);
    }

    public async Task<RequestResult> PostActorRoleType(RoleTypeRequest data)
    {
        UserValidator validator = _userService.GetValidator().MustBeAdmin();
        if (!validator.IsValid)
        {
            return RequestResult.Forbidden();
        }

        PersonActorRoleType item = data.CreateActorRoleType();
        await _database.PersonActorRoleTypes.AddAsync(item);
        await _database.SaveChangesAsync();
        
        return RequestResult.Created($"roles/actor/{item.Id}", new RoleTypeResponse(item));
    }
    
    public async Task<RequestResult> DeleteActorRoleType(short id)
    {
        UserValidator validator = _userService.GetValidator().MustBeAdmin();
        if (!validator.IsValid)
        {
            return RequestResult.Forbidden();
        }
        
        PersonActorRoleType? item = await _database.PersonActorRoleTypes.FirstOrDefaultAsync(x => x.Id == id);
        if (item is null)
        {
            return RequestResult.NoContent();
        }

        _database.PersonActorRoleTypes.Attach(item);
        _database.PersonActorRoleTypes.Remove(item);
        await _database.SaveChangesAsync();
        
        return RequestResult.NoContent();
    }
    
    #endregion
    
    
    
    #region Creator
    
    public async Task<RequestResult> GetAllCreatorRoleTypes(RoleTypeQueryParameters query)
    {
        IEnumerable<PersonCreatorRoleType> rawData = await _database.PersonCreatorRoleTypes.ToListAsync();
        IEnumerable<RoleTypeResponse> data = rawData.Select(x => new RoleTypeResponse(x));
        data = query.PrepareData(data);
        return RequestResult.Ok(data);
    }

    public async Task<RequestResult> GetCreatorRoleType(short id)
    {
        PersonCreatorRoleType? item = await _database.PersonCreatorRoleTypes.FirstOrDefaultAsync(x => x.Id == id);
        if (item is null)
        {
            return RequestResult.NotFound();
        }

        RoleTypeResponse data = new RoleTypeResponse(item);
        return RequestResult.Ok(data);
    }

    public async Task<RequestResult> PostCreatorRoleType(RoleTypeRequest data)
    {
        UserValidator validator = _userService.GetValidator().MustBeAdmin();
        if (!validator.IsValid)
        {
            return RequestResult.Forbidden();
        }

        PersonCreatorRoleType item = data.CreateCreatorRoleType();
        await _database.PersonCreatorRoleTypes.AddAsync(item);
        await _database.SaveChangesAsync();
        
        return RequestResult.Created($"roles/creator/{item.Id}", new RoleTypeResponse(item));
    }
    
    public async Task<RequestResult> DeleteCreatorRoleType(short id)
    {
        UserValidator validator = _userService.GetValidator().MustBeAdmin();
        if (!validator.IsValid)
        {
            return RequestResult.Forbidden();
        }
        
        PersonCreatorRoleType? item = await _database.PersonCreatorRoleTypes.FirstOrDefaultAsync(x => x.Id == id);
        if (item is null)
        {
            return RequestResult.NoContent();
        }

        _database.PersonCreatorRoleTypes.Attach(item);
        _database.PersonCreatorRoleTypes.Remove(item);
        await _database.SaveChangesAsync();
        
        return RequestResult.NoContent();
    }
    
    #endregion
    
    #endregion
}