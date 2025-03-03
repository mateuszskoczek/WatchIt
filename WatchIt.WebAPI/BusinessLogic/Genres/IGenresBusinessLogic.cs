using Ardalis.Result;
using WatchIt.DTO.Models.Controllers.Genres.Genre;
using WatchIt.DTO.Query;

namespace WatchIt.WebAPI.BusinessLogic.Genres;

public interface IGenresBusinessLogic
{
    Task<Result<IEnumerable<GenreResponse>>> GetGenres(GenreFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery);
    Task<Result<GenreResponse>> GetGenre(short genreId);
    Task<Result<GenreResponse>> PostGenre(GenreRequest body);
    Task<Result> DeleteGenre(short genreId);
}