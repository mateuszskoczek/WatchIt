using Ardalis.Result;
using Microsoft.EntityFrameworkCore;
using WatchIt.Database.Model.Accounts;
using WatchIt.Database.Model.People;
using WatchIt.Database.Model.Roles;
using WatchIt.DTO;
using WatchIt.DTO.Models.Controllers.People;
using WatchIt.DTO.Models.Controllers.People.Person;
using WatchIt.DTO.Models.Controllers.People.Person.Query;
using WatchIt.DTO.Models.Controllers.Roles;
using WatchIt.DTO.Models.Controllers.Roles.Role.Response;
using WatchIt.DTO.Models.Generics.Image;
using WatchIt.DTO.Models.Generics.Rating;
using WatchIt.DTO.Query;
using WatchIt.WebAPI.Repositories.People;
using WatchIt.WebAPI.Repositories.Roles;
using WatchIt.WebAPI.Services.User;

namespace WatchIt.WebAPI.BusinessLogic.People;

public class PeopleBusinessLogic : IPeopleBusinessLogic
{
    #region SERVICES

    private readonly IUserService _userService;
    private readonly IPeopleRepository _peopleRepository;
    private readonly IRolesRepository _rolesRepository;

    #endregion
    
    
    
    #region CONSTRUCTORS
    
    public PeopleBusinessLogic(IUserService userService, IPeopleRepository peopleRepository, IRolesRepository rolesRepository)
    {
        _userService = userService;
        _peopleRepository = peopleRepository;
        _rolesRepository = rolesRepository;
    }
    
    #endregion
    
    
    
    #region PUBLIC METHODS

    #region Main

    public async Task<Result<IEnumerable<PersonResponse>>> GetPeople(PersonFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, bool includePictures)
    {
        IEnumerable<Person> entities = await _peopleRepository.GetAllAsync(filterQuery, orderQuery, pagingQuery, x => IncludeForPersonResponse(x, includePictures));
        return Result.Success(entities.Select(x => x.ToResponse()));
    }
    
    public async Task<Result<PersonResponse>> GetPerson(long personId, bool includePictures)
    {
        Person? entity = await _peopleRepository.GetAsync(personId, x => IncludeForPersonResponse(x, includePictures));
        return entity switch
        {
            null => Result.NotFound(),
            _ => Result.Success(entity.ToResponse())
        };
    }
    
    public async Task<Result<PersonResponse>> PostPerson(PersonRequest body)
    {
        Person entity = body.ToEntity();
        await _peopleRepository.AddAsync(entity);
        return Result.Created(entity.ToResponse());
    }
    
    public async Task<Result<PersonResponse>> PutPerson(long personId, PersonRequest body)
    {
        return await _peopleRepository.UpdateAsync(personId, x => x.UpdateWithRequest(body)) switch
        {
            false => Result.NotFound(),
            true => Result.Success()
        };
    }
    
    public async Task<Result> DeletePerson(long personId)
    {
        await _peopleRepository.DeleteAsync(personId);
        return Result.NoContent();
    }

    #endregion

    #region Rating

    public async Task<Result<RatingOverallResponse>> GetPersonRating(long personId)
    {
        if (!await _peopleRepository.ExistsAsync(personId))
        {
            return Result.NotFound();
        }
        IEnumerable<RoleRating> roleRatings = await _rolesRepository.GetPersonRoleRatingsAsync(personId);
        return Result.Success(roleRatings.ToOverallResponse());
    }

    public async Task<Result<RatingUserOverallResponse>> GetPersonUserRating(long personId, long accountId)
    {
        if (!await _peopleRepository.ExistsAsync(personId))
        {
            return Result.NotFound();
        }
        IEnumerable<RoleRating> roleRatings = await _rolesRepository.GetPersonRoleRatingsByAccountIdAsync(personId, accountId);
        return Result.Success(roleRatings.ToUserOverallResponse());
    }

    #endregion
    
    #region View count

    public async Task<Result> PutPeopleViewCount(long personId)
    {
        Person? entity = await _peopleRepository.GetAsync(personId);
        if (entity is null)
        {
            return Result.NotFound();
        }

        DateOnly date = DateOnly.FromDateTime(DateTime.UtcNow);
        await _peopleRepository.UpdateOrAddPersonViewCountAsync(personId, date, () => PeopleMappers.CreatePersonViewCountEntity(personId), x => x.ViewCount++);
        return Result.Success();
    }
    
    #endregion
    
    #region Pictures

    public async Task<Result<ImageResponse>> GetPersonPicture(long personId)
    {
        PersonPicture? entity = await _peopleRepository.GetPersonPictureAsync(personId);
        return entity switch
        {
            null => Result.NotFound(),
            _ => Result.Success(entity.ToResponse())
        };
    }

    public async Task<Result<ImageResponse>> PutPersonPicture(long personId, ImageRequest body)
    {
        return await _peopleRepository.ExistsAsync(personId) switch
        {
            true => Result.Success((await _peopleRepository.UpdateOrAddPersonPictureAsync(personId, () => body.ToEntity(personId), x => x.UpdateWithRequest(body))).ToResponse()),
            false => Result.NotFound(),
        };
    }

    public async Task<Result> DeletePersonPicture(long personId)
    {
        await _peopleRepository.DeletePersonPictureAsync(personId);
        return Result.NoContent();
    }
    
    #endregion
    
    #endregion
    
    
    
    #region PRIVATE METHODS

    private IQueryable<Person> IncludeForPersonResponse(IQueryable<Person> query, bool includePictures)
    {
        query = query.Include(y => y.Gender)
                     .Include(y => y.ViewCounts)
                     .Include(y => y.Roles).ThenInclude(y => y.Ratings);
        if (includePictures)
        {
            query = query.Include(y => y.Picture);
        }
        return query;
    }
    
    #endregion
}