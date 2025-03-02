using Ardalis.Result;
using WatchIt.Database.Model.Genres;
using WatchIt.DTO;
using WatchIt.DTO.Models.Controllers.Genres;
using WatchIt.DTO.Models.Controllers.Genres.Genre;
using WatchIt.DTO.Query;
using WatchIt.WebAPI.Repositories.Genres;
using WatchIt.WebAPI.Services.User;

namespace WatchIt.WebAPI.BusinessLogic.Genres;

public class GenresBusinessLogic : IGenresBusinessLogic
{
    #region SERVICES

    private readonly IUserService _userService;
    private readonly IGenresRepository _genresRepository;

    #endregion
    
    
    
    #region CONSTRUCTORS
    
    public GenresBusinessLogic(IUserService userService, IGenresRepository genresRepository)
    {
        _userService = userService;
        _genresRepository = genresRepository;
    }
    
    #endregion
    
    
    
    #region PUBLIC METHODS

    public async Task<Result<IEnumerable<GenreResponse>>> GetGenres(GenreFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery)
    {
        IEnumerable<Genre> entities = await _genresRepository.GetAllAsync(filterQuery, orderQuery, pagingQuery);
        return Result.Success(entities.Select(x => x.ToResponse()));
    }

    public async Task<Result<GenreResponse>> GetGenre(short genreId)
    {
        Genre? entity = await _genresRepository.GetAsync(genreId);
        return entity switch
        {
            null => Result.NotFound(),
            _ => Result.Success(entity.ToResponse())
        };
    }

    public async Task<Result<GenreResponse>> PostGenre(GenreRequest body)
    {
        Genre entity = body.ToEntity();
        await _genresRepository.AddAsync(body.ToEntity());
        return Result.Created(entity.ToResponse());
    }

    public async Task<Result> DeleteGenre(short genreId)
    {
        await _genresRepository.DeleteAsync(x => x.Id == genreId);
        return Result.NoContent();
    }
    
    #endregion
}
