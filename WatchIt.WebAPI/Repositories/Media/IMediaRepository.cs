using WatchIt.Database.Model.Genres;
using WatchIt.Database.Model.Media;
using WatchIt.DTO.Models.Controllers.Genres.Genre;
using WatchIt.DTO.Models.Controllers.Media.Medium.Query;
using WatchIt.DTO.Query;

namespace WatchIt.WebAPI.Repositories.Media;

public interface IMediaRepository : IRepository<Medium>
{
    Task<bool> ExistsAsync(long id);
    Task<Medium?> GetAsync(long id, Func<IQueryable<Medium>, IQueryable<Medium>>? additionalIncludes = null);
    Task<T?> GetAsync<T>(long id, Func<IQueryable<T>, IQueryable<T>>? additionalIncludes = null) where T : Medium;
    Task<IEnumerable<Medium>> GetAllAsync(MediumFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, Func<IQueryable<Medium>, IQueryable<Medium>>? additionalIncludes = null);
    Task<IEnumerable<MediumMovie>> GetAllMoviesAsync(MediumMovieFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, Func<IQueryable<MediumMovie>, IQueryable<MediumMovie>>? additionalIncludes = null);
    Task<IEnumerable<MediumSeries>> GetAllSeriesAsync(MediumSeriesFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, Func<IQueryable<MediumSeries>, IQueryable<MediumSeries>>? additionalIncludes = null);
    Task<bool> UpdateAsync<T>(long id, Action<T> updateFunc) where T : Medium;
    
    Task<IEnumerable<Genre>> GetMediumGenresAsync(long id, GenreFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, Func<IQueryable<Genre>, IQueryable<Genre>>? additionalIncludes = null);
    Task AddMediumGenreAsync(long mediumId, short genreId);
    Task DeleteMediumGenreAsync(long mediumId, short genreId);
    
    Task<IEnumerable<Medium>> GetAllRatedByAccountAsync(long accountId, MediumFilterQuery filterQuery, MediumUserRatedFilterQuery<Medium> userRatedFilterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, Func<IQueryable<Medium>, IQueryable<Medium>>? additionalIncludes = null);
    Task<IEnumerable<MediumMovie>> GetAllMoviesRatedByAccountAsync(long accountId, MediumMovieFilterQuery filterQuery, MediumUserRatedFilterQuery<MediumMovie> userRatedFilterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, Func<IQueryable<MediumMovie>, IQueryable<MediumMovie>>? additionalIncludes = null);
    Task<IEnumerable<MediumSeries>> GetAllSeriesRatedByAccountAsync(long accountId, MediumSeriesFilterQuery filterQuery, MediumUserRatedFilterQuery<MediumSeries> userRatedFilterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, Func<IQueryable<MediumSeries>, IQueryable<MediumSeries>>? additionalIncludes = null);
    Task<MediumRating?> GetMediumUserRatingAsync(long mediumId, long accountId, Func<IQueryable<MediumRating>, IQueryable<MediumRating>>? additionalIncludes = null);
    Task<MediumRating> UpdateOrAddMediumRatingAsync(long mediumId, long accountId, Func<MediumRating> addFunc, Action<MediumRating> updateFunc);
    Task DeleteMediumUserRatingAsync(long mediumId, long accountId);

    Task<MediumViewCount> UpdateOrAddMediumViewCountAsync(long mediumId, DateOnly date, Func<MediumViewCount> addFunc, Action<MediumViewCount> updateFunc);

    Task<MediumPicture?> GetMediumPictureAsync(long id, Func<IQueryable<MediumPicture>, IQueryable<MediumPicture>>? additionalIncludes = null);
    Task<MediumPicture> UpdateOrAddMediumPictureAsync(long id, Func<MediumPicture> addFunc, Action<MediumPicture> updateFunc);
    Task DeleteMediumPictureAsync(long id);
}