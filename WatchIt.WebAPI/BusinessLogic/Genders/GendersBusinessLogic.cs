using Ardalis.Result;
using Microsoft.EntityFrameworkCore;
using WatchIt.Database.Model.Genders;
using WatchIt.DTO;
using WatchIt.DTO.Models.Controllers.Genders;
using WatchIt.DTO.Models.Controllers.Genders.Gender;
using WatchIt.DTO.Query;
using WatchIt.WebAPI.Repositories.Genders;
using WatchIt.WebAPI.Services.User;

namespace WatchIt.WebAPI.BusinessLogic.Genders;

public class GendersBusinessLogic : IGendersBusinessLogic
{
    #region SERVICES

    private readonly IUserService _userService;
    private readonly IGendersRepository _gendersRepository;

    #endregion
    
    
    
    #region CONSTRUCTORS
    
    public GendersBusinessLogic(IUserService userService, IGendersRepository gendersRepository)
    {
        _userService = userService;
        _gendersRepository = gendersRepository;
    }
    
    #endregion
    
    
    
    #region PUBLIC METHODS

    public async Task<Result<IEnumerable<GenderResponse>>> GetGenders(GenderFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery)
    {
        IEnumerable<Gender> entities = await _gendersRepository.GetAllAsync(filterQuery, orderQuery, pagingQuery);
        return Result.Success(entities.Select(x => x.ToResponse()));
    }

    public async Task<Result<GenderResponse>> GetGender(short genderId)
    {
        Gender? entity = await _gendersRepository.GetAsync(genderId);
        return entity switch
        {
            null => Result.NotFound(),
            _ => Result.Success(entity.ToResponse())
        };
    }

    public async Task<Result<GenderResponse>> PostGender(GenderRequest body)
    {
        Gender entity = body.ToEntity();
        await _gendersRepository.AddAsync(entity);
        return Result.Created(entity.ToResponse());
    }

    public async Task<Result> DeleteGender(short genderId)
    {
        await _gendersRepository.DeleteAsync(x => x.Id == genderId);
        return Result.NoContent();
    }
    
    #endregion
}