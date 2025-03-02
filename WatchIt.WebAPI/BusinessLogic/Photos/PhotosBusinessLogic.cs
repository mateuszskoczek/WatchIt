using Ardalis.Result;
using Microsoft.EntityFrameworkCore;
using WatchIt.Database.Model.Photos;
using WatchIt.DTO.Models.Controllers.Photos;
using WatchIt.DTO.Models.Controllers.Photos.Photo;
using WatchIt.DTO.Models.Controllers.Photos.PhotoBackground;
using WatchIt.DTO.Models.Generics.Image;
using WatchIt.DTO.Query;
using WatchIt.WebAPI.Repositories.Photos;

namespace WatchIt.WebAPI.BusinessLogic.Photos;

public class PhotosBusinessLogic : IPhotosBusinessLogic
{
    #region SERVICES

    private readonly IPhotosRepository _photosRepository;

    #endregion
    
    
    
    #region CONSTRUCTORS
    
    public PhotosBusinessLogic(IPhotosRepository photosRepository)
    {
        _photosRepository = photosRepository;
    }
    
    #endregion



    #region PUBLIC METHODS

    #region Main

    public async Task<Result<PhotoResponse>> GetPhoto(Guid photoId)
    {
        Photo? entity = await _photosRepository.GetAsync(photoId, x => x.Include(y => y.Background));
        return entity switch
        {
            null => Result.NotFound(),
            _ => Result.Success(entity.ToResponse())
        };
    }

    public async Task<Result<IEnumerable<PhotoResponse>>> GetPhotos(PhotoFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery)
    {
        IEnumerable<Photo> entities = await _photosRepository.GetAllAsync(filterQuery, orderQuery, pagingQuery, x => x.Include(y => y.Background));
        return Result.Success(entities.Select(x => x.ToResponse()));
    }
    
    public async Task<Result<PhotoResponse>> PostPhoto(PhotoRequest body)
    {
        Photo entity = body.ToEntity();
        await _photosRepository.AddAsync(entity);
        if (body.BackgroundData is not null)
        {
            await _photosRepository.AddPhotoBackgroundAsync(body.BackgroundData.ToEntity(entity.Id));
        }
        return Result.Created(entity.ToResponse());
    }
    
    public async Task<Result<PhotoResponse>> PutPhoto(Guid photoId, PhotoRequest body)
    {
        bool success = await _photosRepository.UpdateAsync(photoId, x => x.UpdateWithRequest(body));
        if (success)
        {
            if (body.BackgroundData is not null)
            {
                await _photosRepository.UpdateOrAddPhotoBackgroundAsync(photoId, () => body.BackgroundData.ToEntity(photoId), x => x.UpdateWithRequest(body.BackgroundData));
            }
            else
            {
                await _photosRepository.DeletePhotoBackgroundAsync(photoId);
            }
            return Result.Success();
        }
        return Result.NotFound();
    }
    
    public async Task<Result> DeletePhoto(Guid photoId)
    {
        await _photosRepository.DeleteAsync(photoId);
        return Result.NoContent();
    }

    #endregion
    
    #region Background
    
    public async Task<Result<PhotoResponse>> GetPhotoBackground()
    {
        Photo? entity = await _photosRepository.GetPhotoRandomBackgroundAsync(x => x.Include(y => y.Background));
        return entity switch
        {
            null => Result.NotFound(),
            _ => Result.Success(entity.ToResponse())
        };
    }
    
    public async Task<Result<PhotoBackgroundResponse>> PutPhotoBackground(Guid photoId, PhotoBackgroundRequest body)
    {
        if (await _photosRepository.ExistsAsync(photoId))
        {
            return Result.NotFound();
        }
        PhotoBackground entity = await _photosRepository.UpdateOrAddPhotoBackgroundAsync(photoId, () => body.ToEntity(photoId), x => x.UpdateWithRequest(body));
        return Result.Success(entity.ToResponse());
    }

    public async Task<Result> DeletePhotoBackground(Guid photoId)
    {
        await _photosRepository.DeletePhotoBackgroundAsync(photoId);
        return Result.NoContent();
    }
    
    #endregion
    
    #endregion
}