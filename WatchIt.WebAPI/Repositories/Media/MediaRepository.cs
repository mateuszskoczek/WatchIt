using Microsoft.EntityFrameworkCore;
using WatchIt.Database;
using WatchIt.Database.Model.Genres;
using WatchIt.Database.Model.Media;
using WatchIt.Database.Model.Photos;
using WatchIt.DTO.Models.Controllers.Genres.Genre;
using WatchIt.DTO.Models.Controllers.Media.Medium;
using WatchIt.DTO.Models.Controllers.Media.Medium.Query;
using WatchIt.DTO.Query;

namespace WatchIt.WebAPI.Repositories.Media;

public class MediaRepository : Repository<Medium>, IMediaRepository
{
    #region CONSTRUCTORS
    
    public MediaRepository(DatabaseContext database) : base(database) {}
    
    #endregion
    
    
    
    #region PUBLIC METHODS
    
    #region Main
    
    public async Task<bool> ExistsAsync(long id) =>
        await DefaultSet.AnyAsync(x => x.Id == id);

    public async Task<Medium?> GetAsync(long id, Func<IQueryable<Medium>, IQueryable<Medium>>? additionalIncludes = null) =>
        await DefaultSet.Include(additionalIncludes)
                        .FirstOrDefaultAsync(x => x.Id == id);
    
    public async Task<T?> GetAsync<T>(long id, Func<IQueryable<T>, IQueryable<T>>? additionalIncludes = null) where T : Medium =>
        await DefaultSet.OfType<T>()
                        .Include(additionalIncludes)
                        .FirstOrDefaultAsync(x => x.Id == id);
    
    public async Task<IEnumerable<Medium>> GetAllAsync(MediumFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, Func<IQueryable<Medium>, IQueryable<Medium>>? additionalIncludes = null) =>
        await DefaultSet.ApplyFilter(filterQuery)
                        .OrderByDescending(x => x.Id)
                        .ApplyOrder(orderQuery, MediumOrderKeys.Base<Medium>(), MediumOrderKeys.Medium)
                        .ApplyPaging(pagingQuery)
                        .Include(additionalIncludes)
                        .ToListAsync();
    
    public async Task<IEnumerable<MediumMovie>> GetAllMoviesAsync(MediumMovieFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, Func<IQueryable<MediumMovie>, IQueryable<MediumMovie>>? additionalIncludes = null) =>
        await DefaultSet.OfType<MediumMovie>()
                        .ApplyFilter(filterQuery)
                        .ApplyOrder(orderQuery, MediumOrderKeys.Base<MediumMovie>(), MediumOrderKeys.MediumMovie)
                        .ApplyPaging(pagingQuery)
                        .Include(additionalIncludes)
                        .ToListAsync();
    
    public async Task<IEnumerable<MediumSeries>> GetAllSeriesAsync(MediumSeriesFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, Func<IQueryable<MediumSeries>, IQueryable<MediumSeries>>? additionalIncludes = null) =>
        await DefaultSet.OfType<MediumSeries>()
                        .ApplyFilter(filterQuery)
                        .ApplyOrder(orderQuery, MediumOrderKeys.Base<MediumSeries>(), MediumOrderKeys.MediumSeries)
                        .ApplyPaging(pagingQuery)
                        .Include(additionalIncludes)
                        .ToListAsync();

    public async Task<bool> UpdateAsync<T>(long id, Action<T> updateFunc) where T : Medium
    {
        T? entity = await GetAsync<T>(id);
        if (entity is null)
        {
            return false;
        }
        
        updateFunc(entity);
        DefaultSet.Update(entity);
        await Database.SaveChangesAsync();
        return true;
    }
    
    #endregion
    
    #region Genres

    public async Task<IEnumerable<Genre>> GetMediumGenresAsync(long id, GenreFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, Func<IQueryable<Genre>, IQueryable<Genre>>? additionalIncludes = null) =>
        await Database.Set<MediumGenre>()
                      .Where(x => x.MediumId == id)
                      .Select(x => x.Genre)
                      .ApplyFilter(filterQuery)
                      .ApplyOrder(orderQuery, GenreOrderKeys.Base)
                      .ApplyPaging(pagingQuery)
                      .Include(additionalIncludes)
                      .ToListAsync();

    public async Task AddMediumGenreAsync(long mediumId, short genreId)
    {
        if (!Database.Set<MediumGenre>().Any(x => x.MediumId == mediumId && x.GenreId == genreId))
        {
            await AddAsync(new MediumGenre { MediumId = mediumId, GenreId = genreId });
        }
    }
    
    public async Task DeleteMediumGenreAsync(long mediumId, short genreId) =>
        await DeleteAsync(new MediumGenre { MediumId = mediumId, GenreId = genreId });

    #endregion
    
    #region Rating
    
    public async Task<IEnumerable<Medium>> GetAllRatedByAccountAsync(long accountId, MediumFilterQuery filterQuery, MediumUserRatedFilterQuery<Medium> userRatedFilterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, Func<IQueryable<Medium>, IQueryable<Medium>>? additionalIncludes = null) =>
        await DefaultSet.Where(x => x.Ratings
                                     .Any(y => y.AccountId == accountId))
                        .ApplyFilter(filterQuery)
                        .ApplyFilter(userRatedFilterQuery)
                        .ApplyOrder(orderQuery, MediumOrderKeys.Base<Medium>(), MediumOrderKeys.Medium, MediumOrderKeys.MediumUserRated<Medium>(accountId))
                        .ApplyPaging(pagingQuery)
                        .Include(additionalIncludes)
                        .ToListAsync();
    
    public async Task<IEnumerable<MediumMovie>> GetAllMoviesRatedByAccountAsync(long accountId, MediumMovieFilterQuery filterQuery, MediumUserRatedFilterQuery<MediumMovie> userRatedFilterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, Func<IQueryable<MediumMovie>, IQueryable<MediumMovie>>? additionalIncludes = null) =>
        await DefaultSet.OfType<MediumMovie>()
                        .Where(x => x.Ratings
                                     .Any(y => y.AccountId == accountId))
                        .ApplyFilter(filterQuery)
                        .ApplyFilter(userRatedFilterQuery)
                        .ApplyOrder(orderQuery, MediumOrderKeys.Base<MediumMovie>(), MediumOrderKeys.MediumMovie, MediumOrderKeys.MediumUserRated<MediumMovie>(accountId))
                        .ApplyPaging(pagingQuery)
                        .Include(additionalIncludes)
                        .ToListAsync();
    
    public async Task<IEnumerable<MediumSeries>> GetAllSeriesRatedByAccountAsync(long accountId, MediumSeriesFilterQuery filterQuery, MediumUserRatedFilterQuery<MediumSeries> userRatedFilterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, Func<IQueryable<MediumSeries>, IQueryable<MediumSeries>>? additionalIncludes = null) =>
        await DefaultSet.OfType<MediumSeries>()
                        .Where(x => x.Ratings
                                     .Any(y => y.AccountId == accountId))
                        .ApplyFilter(filterQuery)
                        .ApplyFilter(userRatedFilterQuery)
                        .ApplyOrder(orderQuery, MediumOrderKeys.Base<MediumSeries>(), MediumOrderKeys.MediumSeries, MediumOrderKeys.MediumUserRated<MediumSeries>(accountId))
                        .ApplyPaging(pagingQuery)
                        .Include(additionalIncludes)
                        .ToListAsync();

    public async Task<MediumRating?> GetMediumUserRatingAsync(long mediumId, long accountId, Func<IQueryable<MediumRating>, IQueryable<MediumRating>>? additionalIncludes = null) =>
        await Database.Set<MediumRating>()
                      .Include(additionalIncludes)
                      .FirstOrDefaultAsync(x => x.MediumId == mediumId && x.AccountId == accountId);

    public async Task<MediumRating> UpdateOrAddMediumRatingAsync(long mediumId, long accountId, Func<MediumRating> addFunc, Action<MediumRating> updateFunc) =>
        await UpdateOrAddAsync(await GetMediumUserRatingAsync(mediumId, accountId), addFunc, updateFunc);
    
    public async Task DeleteMediumUserRatingAsync(long mediumId, long accountId) =>
        await DeleteAsync<MediumRating>(x => x.MediumId == mediumId && x.AccountId == accountId);

    #endregion
    
    #region View count
    
    public async Task<MediumViewCount> UpdateOrAddMediumViewCountAsync(long mediumId, DateOnly date, Func<MediumViewCount> addFunc, Action<MediumViewCount> updateFunc) =>
        await UpdateOrAddAsync(await Database.Set<MediumViewCount>()
                                             .FirstOrDefaultAsync(x => x.MediumId == mediumId && x.Date == date), addFunc, updateFunc);
    
    #endregion
    
    #region Picture
    
    public async Task<MediumPicture?> GetMediumPictureAsync(long id, Func<IQueryable<MediumPicture>, IQueryable<MediumPicture>>? additionalIncludes = null) =>
        await Database.MediumPictures
                      .Include(additionalIncludes)
                      .FirstOrDefaultAsync(x => x.MediumId == id);
    
    public async Task<MediumPicture> UpdateOrAddMediumPictureAsync(long id, Func<MediumPicture> addFunc, Action<MediumPicture> updateFunc) =>
        await UpdateOrAddAsync(await GetMediumPictureAsync(id), addFunc, updateFunc);

    public async Task DeleteMediumPictureAsync(long id) =>
        await DeleteAsync(new MediumPicture { MediumId = id });
    
    #endregion

    #endregion
}