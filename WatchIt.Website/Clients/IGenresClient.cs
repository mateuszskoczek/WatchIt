using Refit;
using WatchIt.DTO.Models.Controllers.Genres.Genre;
using WatchIt.DTO.Query;

namespace WatchIt.Website.Clients;

public interface IGenresClient
{
    [Get("/")]
    Task<IApiResponse<IEnumerable<GenreResponse>>> GetGenres([Query] GenreFilterQuery? filterQuery = null, [Query] OrderQuery? orderQuery = null, [Query] PagingQuery? pagingQuery = null);

    [Get("/{id}")]
    Task<IApiResponse<GenreResponse>> GetGenre(short id);

    [Post("/")]
    Task<IApiResponse<GenreResponse>> PostGenre([Authorize]string token, [Body] GenreRequest body);

    [Delete("/{id}")]
    Task<IApiResponse> DeleteGenre([Authorize]string token, short id);
}