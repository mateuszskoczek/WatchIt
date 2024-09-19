using Microsoft.EntityFrameworkCore;
using SimpleToolkit.Extensions;
using WatchIt.Common.Model.Genres;
using WatchIt.Common.Model.Media;
using WatchIt.Database;
using WatchIt.Database.Model.Media;
using WatchIt.WebAPI.Services.Controllers.Common;
using WatchIt.WebAPI.Services.Utility.User;

namespace WatchIt.WebAPI.Services.Controllers.Media;

public class MediaControllerService(DatabaseContext database, IUserService userService) : IMediaControllerService
{
    #region PUBLIC METHODS

    public async Task<RequestResult> GetMedia(long mediaId)
    {
        Database.Model.Media.Media? item = await database.Media.FirstOrDefaultAsync(x => x.Id == mediaId);
        if (item is null)
        {
            return RequestResult.NotFound();
        }

        MediaMovie? movie = await database.MediaMovies.FirstOrDefaultAsync(x => x.Id == mediaId);

        MediaResponse mediaResponse = new MediaResponse(item, movie is not null ? MediaType.Movie : MediaType.Series);

        return RequestResult.Ok(mediaResponse);
    }
    
    public async Task<RequestResult> GetGenres(long mediaId)
    {
        Database.Model.Media.Media? item = await database.Media.FirstOrDefaultAsync(x => x.Id == mediaId);
        if (item is null)
        {
            return RequestResult.NotFound();
        }

        IEnumerable<GenreResponse> genres = item.MediaGenres.Select(x => new GenreResponse(x.Genre));
        return RequestResult.Ok(genres);
    }

    public async Task<RequestResult> PostGenre(long mediaId, short genreId)
    {
        UserValidator validator = userService.GetValidator().MustBeAdmin();
        if (!validator.IsValid)
        {
            return RequestResult.Forbidden();
        }
        
        Database.Model.Media.Media? mediaItem = await database.Media.FirstOrDefaultAsync(x => x.Id == mediaId);
        Database.Model.Common.Genre? genreItem = await database.Genres.FirstOrDefaultAsync(x => x.Id == genreId);
        if (mediaItem is null || genreItem is null)
        {
            return RequestResult.NotFound();
        }

        await database.MediaGenres.AddAsync(new MediaGenre
        {
            GenreId = genreId,
            MediaId = mediaId,
        });
        await database.SaveChangesAsync();
        
        return RequestResult.Ok();
    }
    
    public async Task<RequestResult> DeleteGenre(long mediaId, short genreId)
    {
        UserValidator validator = userService.GetValidator().MustBeAdmin();
        if (!validator.IsValid)
        {
            return RequestResult.Forbidden();
        }
        
        MediaGenre? item = await database.MediaGenres.FirstOrDefaultAsync(x => x.MediaId == mediaId && x.GenreId == genreId);
        if (item is null)
        {
            return RequestResult.NotFound();
        }

        database.MediaGenres.Attach(item);
        database.MediaGenres.Remove(item);
        await database.SaveChangesAsync();
        
        return RequestResult.Ok();
    }

    public async Task<RequestResult> GetPhoto(Guid id)
    {
        MediaPhotoImage? item = await database.MediaPhotoImages.FirstOrDefaultAsync(x => x.Id == id);
        if (item is null)
        {
            return RequestResult.NotFound();
        }
        
        MediaPhotoResponse data = new MediaPhotoResponse(item);
        return RequestResult.Ok(data);
    }
    
    public async Task<RequestResult> GetPhotos(MediaPhotoQueryParameters query)
    {
        IEnumerable<MediaPhotoResponse> data = await database.MediaPhotoImages.Select(x => new MediaPhotoResponse(x)).ToListAsync();
        data = query.PrepareData(data);
        return RequestResult.Ok(data);
    }
    
    public Task<RequestResult> GetRandomBackgroundPhoto()
    {
        MediaPhotoImage? image = database.MediaPhotoImages.Where(x => x.MediaPhotoImageBackground != null && x.MediaPhotoImageBackground.IsUniversalBackground).Random();
        if (image is null)
        {
            return Task.FromResult<RequestResult>(RequestResult.NotFound());
        }

        MediaPhotoResponse data = new MediaPhotoResponse(image);
        return Task.FromResult<RequestResult>(RequestResult.Ok(data));
    }
    
    public Task<RequestResult> GetMediaRandomBackgroundPhoto(long id)
    {
        MediaPhotoImage? image = database.MediaPhotoImages.Where(x => x.MediaId == id && x.MediaPhotoImageBackground != null).Random();
        if (image is null)
        {
            return Task.FromResult<RequestResult>(RequestResult.NotFound());
        }

        MediaPhotoResponse data = new MediaPhotoResponse(image);
        return Task.FromResult<RequestResult>(RequestResult.Ok(data));
    }

    public async Task<RequestResult> GetPoster(long id)
    {
        Database.Model.Media.Media? media = await database.Media.FirstOrDefaultAsync(x => x.Id == id);
        if (media is null)
        {
            return RequestResult.BadRequest();
        }

        MediaPosterImage? poster = media.MediaPosterImage;
        if (poster is null)
        {
            return RequestResult.NotFound();
        }

        MediaPosterResponse data = new MediaPosterResponse(poster);
        return RequestResult.Ok(data);
    }

    public async Task<RequestResult> PutPoster(long id, MediaPosterRequest data)
    {
        UserValidator validator = userService.GetValidator().MustBeAdmin();
        if (!validator.IsValid)
        {
            return RequestResult.Forbidden();
        }
        
        Database.Model.Media.Media? media = await database.Media.FirstOrDefaultAsync(x => x.Id == id);
        if (media is null)
        {
            return RequestResult.BadRequest();
        }

        if (media.MediaPosterImage is null)
        {
            MediaPosterImage image = data.CreateMediaPosterImage();
            await database.MediaPosterImages.AddAsync(image);
            await database.SaveChangesAsync();

            media.MediaPosterImageId = image.Id;
        }
        else
        {
            data.UpdateMediaPosterImage(media.MediaPosterImage);
        }
        
        await database.SaveChangesAsync();

        return RequestResult.Ok();
    }

    public async Task<RequestResult> DeletePoster(long id)
    {
        UserValidator validator = userService.GetValidator().MustBeAdmin();
        if (!validator.IsValid)
        {
            return RequestResult.Forbidden();
        }
        
        Database.Model.Media.Media? media = await database.Media.FirstOrDefaultAsync(x => x.Id == id);

        if (media?.MediaPosterImage != null)
        {
            database.MediaPosterImages.Attach(media.MediaPosterImage);
            database.MediaPosterImages.Remove(media.MediaPosterImage);
            await database.SaveChangesAsync();
        }

        return RequestResult.NoContent();
    }

    public async Task<RequestResult> PostPhoto(MediaPhotoRequest data)
    {
        UserValidator validator = userService.GetValidator().MustBeAdmin();
        if (!validator.IsValid)
        {
            return RequestResult.Forbidden();
        }

        MediaPhotoImage item = data.CreateMediaPhotoImage();
        await database.MediaPhotoImages.AddAsync(item);
        await database.SaveChangesAsync();

        MediaPhotoImageBackground? background = data.CreateMediaPhotoImageBackground(item.Id);
        if (background is not null)
        {
            await database.MediaPhotoImageBackgrounds.AddAsync(background);
            await database.SaveChangesAsync();
        }
        
        return RequestResult.Created($"photos/{item.Id}", new MediaPhotoResponse(item));
    }
    
    public async Task<RequestResult> PutPhoto(Guid photoId, MediaPhotoRequest data)
    {
        UserValidator validator = userService.GetValidator().MustBeAdmin();
        if (!validator.IsValid)
        {
            return RequestResult.Forbidden();
        }

        MediaPhotoImage? item = await database.MediaPhotoImages.FirstOrDefaultAsync(x => x.Id == photoId);
        if (item is null)
        {
            return RequestResult.NotFound();
        }

        data.UpdateMediaPhotoImage(item);
        if (item.MediaPhotoImageBackground is null && data.Background is not null)
        {
            MediaPhotoImageBackground background = data.CreateMediaPhotoImageBackground(item.Id)!;
            await database.MediaPhotoImageBackgrounds.AddAsync(background);
        }
        else if (item.MediaPhotoImageBackground is not null && data.Background is null)
        {
            database.MediaPhotoImageBackgrounds.Attach(item.MediaPhotoImageBackground);
            database.MediaPhotoImageBackgrounds.Remove(item.MediaPhotoImageBackground);
        }
        else if (item.MediaPhotoImageBackground is not null && data.Background is not null)
        {
            data.UpdateMediaPhotoImageBackground(item.MediaPhotoImageBackground);
        }
        await database.SaveChangesAsync();
        
        return RequestResult.Ok();
    }
    
    public async Task<RequestResult> DeletePhoto(Guid photoId)
    {
        UserValidator validator = userService.GetValidator().MustBeAdmin();
        if (!validator.IsValid)
        {
            return RequestResult.Forbidden();
        }
        
        MediaPhotoImage? item = await database.MediaPhotoImages.FirstOrDefaultAsync(x => x.Id == photoId);
        if (item is null)
        {
            return RequestResult.NotFound();
        }

        if (item.MediaPhotoImageBackground is not null)
        {
            database.MediaPhotoImageBackgrounds.Attach(item.MediaPhotoImageBackground);
            database.MediaPhotoImageBackgrounds.Remove(item.MediaPhotoImageBackground);
            await database.SaveChangesAsync();
        }

        database.MediaPhotoImages.Attach(item);
        database.MediaPhotoImages.Remove(item);
        await database.SaveChangesAsync();
        
        return RequestResult.Ok();
    }
    
    #endregion
}