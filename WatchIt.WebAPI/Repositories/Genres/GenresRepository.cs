using Microsoft.EntityFrameworkCore;
using WatchIt.Database;
using WatchIt.Database.Model.Genres;
using WatchIt.DTO.Models.Controllers.Genres.Genre;
using WatchIt.DTO.Query;

namespace WatchIt.WebAPI.Repositories.Genres;

public class GenresRepository : Repository<Genre>, IGenresRepository
{
    #region CONSTRUCTORS
    
    public GenresRepository(DatabaseContext database) : base(database) {}
    
    #endregion
    
    
    
    #region PUBLIC METHODS
    
    public async Task<bool> ExistsAsync(short id) =>
        await DefaultSet.AnyAsync(x => x.Id == id);

    public async Task<Genre?> GetAsync(short id, Func<IQueryable<Genre>, IQueryable<Genre>>? additionalIncludes = null) =>
        await DefaultSet.Include(additionalIncludes)
                        .FirstOrDefaultAsync(x => x.Id == id);
    
    public async Task<IEnumerable<Genre>> GetAllAsync(GenreFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, Func<IQueryable<Genre>, IQueryable<Genre>>? additionalIncludes = null) =>
        await DefaultSet.ApplyFilter(filterQuery)
                        .ApplyOrder(orderQuery, GenreOrderKeys.Base)
                        .ApplyPaging(pagingQuery)
                        .Include(additionalIncludes)
                        .ToListAsync();
    
    #endregion
}