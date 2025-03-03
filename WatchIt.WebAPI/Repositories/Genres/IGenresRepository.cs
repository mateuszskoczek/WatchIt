using WatchIt.Database.Model.Genres;
using WatchIt.DTO.Models.Controllers.Genres.Genre;
using WatchIt.DTO.Query;

namespace WatchIt.WebAPI.Repositories.Genres;

public interface IGenresRepository : IRepository<Genre>
{
    Task<bool> ExistsAsync(short id);
    Task<Genre?> GetAsync(short id, Func<IQueryable<Genre>, IQueryable<Genre>>? additionalIncludes = null);
    Task<IEnumerable<Genre>> GetAllAsync(GenreFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, Func<IQueryable<Genre>, IQueryable<Genre>>? additionalIncludes = null);
}