using WatchIt.Database.Model.Photos;
using WatchIt.DTO.Models.Controllers.Photos.Photo;
using WatchIt.DTO.Query;

namespace WatchIt.WebAPI.Repositories.Photos;

public interface IPhotosRepository : IRepository<Photo>
{
    Task<bool> ExistsAsync(Guid id);
    Task<Photo?> GetAsync(Guid id, Func<IQueryable<Photo>, IQueryable<Photo>>? additionalIncludes = null);
    Task<IEnumerable<Photo>> GetAllAsync(PhotoFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, Func<IQueryable<Photo>, IQueryable<Photo>>? additionalIncludes = null);
    Task<Photo?> GetPhotoRandomBackgroundAsync(Func<IQueryable<Photo>, IQueryable<Photo>>? additionalIncludes = null);
    Task<bool> UpdateAsync(Guid id, Action<Photo> updateFunc);
    Task DeleteAsync(Guid id);

    Task AddPhotoBackgroundAsync(PhotoBackground photoBackground);
    Task<PhotoBackground> UpdateOrAddPhotoBackgroundAsync(Guid photoId, Func<PhotoBackground> addFunc, Action<PhotoBackground> updateFunc);
    Task DeletePhotoBackgroundAsync(Guid photoId);
    
    Task<Photo> GetPhotoRandomBackgroundByMediumAsync(long mediumId, Func<IQueryable<Photo>, IQueryable<Photo>>? additionalIncludes = null);
}