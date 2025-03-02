using Microsoft.EntityFrameworkCore;
using WatchIt.Database;
using WatchIt.Database.Model.Photos;
using WatchIt.DTO.Models.Controllers.Photos.Photo;
using WatchIt.DTO.Query;

namespace WatchIt.WebAPI.Repositories.Photos;

public class PhotosRepository : Repository<Photo>, IPhotosRepository
{
    #region CONSTRUCTORS
    
    public PhotosRepository(DatabaseContext database) : base(database) {}
    
    #endregion
    
    
    
    #region PUBLIC METHODS

    #region Main
    
    public async Task<bool> ExistsAsync(Guid id) =>
        await DefaultSet.AnyAsync(x => x.Id == id);

    public async Task<Photo?> GetAsync(Guid id, Func<IQueryable<Photo>, IQueryable<Photo>>? additionalIncludes = null) =>
        await DefaultSet.Include(additionalIncludes)
                        .FirstOrDefaultAsync(x => x.Id == id);

    public async Task<IEnumerable<Photo>> GetAllAsync(PhotoFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, Func<IQueryable<Photo>, IQueryable<Photo>>? additionalIncludes = null) =>
        await DefaultSet.ApplyFilter(filterQuery)
                        .ApplyOrder(orderQuery, PhotoOrderKeys.Base)
                        .ApplyPaging(pagingQuery)
                        .Include(additionalIncludes)
                        .ToListAsync();

    public async Task<Photo?> GetPhotoRandomBackgroundAsync(Func<IQueryable<Photo>, IQueryable<Photo>>? additionalIncludes = null)
    {
        IQueryable<Photo> query = DefaultSet.Where(x => x.Background != null && x.Background.IsUniversal);
        int count = await query.CountAsync();
        int randomIndex = Random.Shared.Next(count);
        return await query.Include(additionalIncludes)
                          .ElementAtAsync(randomIndex);
    }
    
    public async Task<bool> UpdateAsync(Guid id, Action<Photo> updateFunc)
    {
        Photo? entity = await GetAsync(id);
        if (entity is null)
        {
            return false;
        }
        
        updateFunc(entity);
        DefaultSet.Update(entity);
        await Database.SaveChangesAsync();
        return true;
    }

    public async Task DeleteAsync(Guid id)
    {
        await DeleteAsync<PhotoBackground>(x => x.PhotoId == id);
        await DeleteAsync(x => x.Id == id);
    }

    #endregion

    #region Background

    public async Task AddPhotoBackgroundAsync(PhotoBackground photoBackground)
    {
        await Database.PhotoBackgrounds.AddAsync(photoBackground);
        await Database.SaveChangesAsync();
    }
    
    public async Task<PhotoBackground> UpdateOrAddPhotoBackgroundAsync(Guid photoId, Func<PhotoBackground> addFunc, Action<PhotoBackground> updateFunc)
    {
        PhotoBackground? entity = await Database.PhotoBackgrounds.FirstOrDefaultAsync(x => x.PhotoId == photoId);
        if (entity is null)
        {
            entity = addFunc();
            await Database.PhotoBackgrounds.AddAsync(entity);
        }
        else
        {
            updateFunc(entity);
            Database.PhotoBackgrounds.Update(entity);
        }
        await Database.SaveChangesAsync();
        return entity;
    }
    
    public async Task DeletePhotoBackgroundAsync(Guid photoId) =>
        await DeleteAsync<PhotoBackground>(x => x.PhotoId == photoId);

    #endregion
    
    #region Medium

    public async Task<Photo> GetPhotoRandomBackgroundByMediumAsync(long mediumId, Func<IQueryable<Photo>, IQueryable<Photo>>? additionalIncludes = null)
    {
        IQueryable<Photo> query = DefaultSet.Where(x => x.MediumId == mediumId && x.Background != null);
        int count = await query.CountAsync();
        int randomIndex = Random.Shared.Next(count);
        return await query.Include(additionalIncludes)
                          .ElementAtAsync(randomIndex);
    }

    #endregion

    #endregion
}