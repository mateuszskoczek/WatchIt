using Microsoft.EntityFrameworkCore;
using WatchIt.Database;
using WatchIt.Database.Model.Genders;
using WatchIt.DTO.Models.Controllers.Genders.Gender;
using WatchIt.DTO.Query;

namespace WatchIt.WebAPI.Repositories.Genders;

public class GendersRepository : Repository<Gender>, IGendersRepository
{
    #region CONSTRUCTORS
    
    public GendersRepository(DatabaseContext database) : base(database) {}
    
    #endregion
    
    
    
    #region PUBLIC METHODS
    
    public async Task<bool> ExistsAsync(short id) =>
        await DefaultSet.AnyAsync(x => x.Id == id);

    public async Task<Gender?> GetAsync(short id, Func<IQueryable<Gender>, IQueryable<Gender>>? additionalIncludes = null) =>
        await DefaultSet.Include(additionalIncludes)
                        .FirstOrDefaultAsync(x => x.Id == id);
    
    public async Task<IEnumerable<Gender>> GetAllAsync(GenderFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, Func<IQueryable<Gender>, IQueryable<Gender>>? additionalIncludes = null) =>
        await DefaultSet.ApplyFilter(filterQuery)
                        .ApplyOrder(orderQuery, GenderOrderKeys.Base)
                        .ApplyPaging(pagingQuery)
                        .Include(additionalIncludes)
                        .ToListAsync();
    
    #endregion
}