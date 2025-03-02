using WatchIt.Database.Model.Genders;
using WatchIt.DTO.Models.Controllers.Genders.Gender;
using WatchIt.DTO.Query;

namespace WatchIt.WebAPI.Repositories.Genders;

public interface IGendersRepository : IRepository<Gender>
{
    Task<bool> ExistsAsync(short id);
    Task<Gender?> GetAsync(short id, Func<IQueryable<Gender>, IQueryable<Gender>>? additionalIncludes = null);
    Task<IEnumerable<Gender>> GetAllAsync(GenderFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, Func<IQueryable<Gender>, IQueryable<Gender>>? additionalIncludes = null);
}