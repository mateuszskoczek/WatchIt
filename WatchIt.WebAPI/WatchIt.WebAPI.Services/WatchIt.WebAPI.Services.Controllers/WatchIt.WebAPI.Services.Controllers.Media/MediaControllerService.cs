using Microsoft.EntityFrameworkCore;
using SimpleToolkit.Extensions;
using WatchIt.Common.Model.Genres;
using WatchIt.Common.Model.Media;
using WatchIt.Database;
using WatchIt.Database.Model.Media;
using WatchIt.Database.Model.Rating;
using WatchIt.Database.Model.ViewCount;
using WatchIt.WebAPI.Services.Controllers.Common;
using WatchIt.WebAPI.Services.Utility.User;

namespace WatchIt.WebAPI.Services.Controllers.Media;

public class MediaControllerService(DatabaseContext database, IUserService userService) : IMediaControllerService
{
    #region PUBLIC METHODS

    #region Main

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

    #endregion

    #region Genres

    public async Task<RequestResult> GetMediaGenres(long mediaId)
    {
        Database.Model.Media.Media? item = await database.Media.FirstOrDefaultAsync(x => x.Id == mediaId);
        if (item is null)
        {
            return RequestResult.NotFound();
        }

        IEnumerable<GenreResponse> genres = item.MediaGenres.Select(x => new GenreResponse(x.Genre));
        return RequestResult.Ok(genres);
    }

    public async Task<RequestResult> PostMediaGenre(long mediaId, short genreId)
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
    
    public async Task<RequestResult> DeleteMediaGenre(long mediaId, short genreId)
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

    #endregion

    #region Rating

    public async Task<RequestResult> GetMediaRating(long mediaId)
    {
        Database.Model.Media.Media? item = await database.Media.FirstOrDefaultAsync(x => x.Id == mediaId);
        if (item is null)
        {
            return RequestResult.NotFound();
        }

        double ratingAverage = item.RatingMedia.Any() ? item.RatingMedia.Average(x => x.Rating) : 0;
        long ratingCount = item.RatingMedia.Count();
        MediaRatingResponse ratingResponse = new MediaRatingResponse(ratingAverage, ratingCount);
        
        return RequestResult.Ok(ratingResponse);
    }

    public async Task<RequestResult> GetMediaRatingByUser(long mediaId, long userId)
    {
        Database.Model.Media.Media? item = await database.Media.FirstOrDefaultAsync(x => x.Id == mediaId);
        if (item is null)
        {
            return RequestResult.NotFound();
        }
        
        short? rating = item.RatingMedia.FirstOrDefault(x => x.AccountId == userId)?.Rating;
        if (!rating.HasValue)
        {
            return RequestResult.NotFound();
        }
        
        return RequestResult.Ok(rating.Value);
    }
    
    public async Task<RequestResult> PutMediaRating(long mediaId, MediaRatingRequest data)
    {
        short ratingValue = data.Rating;
        if (ratingValue < 1 || ratingValue > 10)
        {
            return RequestResult.BadRequest();
        }
            
        Database.Model.Media.Media? item = await database.Media.FirstOrDefaultAsync(x => x.Id == mediaId);
        if (item is null)
        {
            return RequestResult.NotFound();
        }
        
        long userId = userService.GetUserId();

        RatingMedia? rating = item.RatingMedia.FirstOrDefault(x => x.AccountId == userId);
        if (rating is not null)
        {
            rating.Rating = ratingValue;
        }
        else
        {
            rating = new RatingMedia
            {
                AccountId = userId,
                MediaId = mediaId,
                Rating = ratingValue
            };
            await database.RatingsMedia.AddAsync(rating);
        }
        await database.SaveChangesAsync();
        
        return RequestResult.Ok();
    }

    public async Task<RequestResult> DeleteMediaRating(long mediaId)
    { 
        long userId = userService.GetUserId();
        
        RatingMedia? item = await database.RatingsMedia.FirstOrDefaultAsync(x => x.MediaId == mediaId && x.AccountId == userId);
        if (item is null)
        {
            return RequestResult.Ok();
        }
        
        database.RatingsMedia.Attach(item);
        database.RatingsMedia.Remove(item);
        await database.SaveChangesAsync();
        
        return RequestResult.Ok();
    }

    #endregion

    #region View count

    public async Task<RequestResult> PostMediaView(long mediaId)
    {
        Database.Model.Media.Media? item = await database.Media.FirstOrDefaultAsync(x => x.Id == mediaId);
        if (item is null)
        {
            return RequestResult.NotFound();
        }

        DateOnly dateNow = DateOnly.FromDateTime(DateTime.Now);
        ViewCountMedia? viewCount = await database.ViewCountsMedia.FirstOrDefaultAsync(x => x.MediaId == mediaId && x.Date == dateNow);
        if (viewCount is null)
        {
            viewCount = new ViewCountMedia
            {
                MediaId = mediaId,
                Date = dateNow,
                ViewCount = 1
            };
            await database.ViewCountsMedia.AddAsync(viewCount);
        }
        else
        {
            viewCount.ViewCount++;
        }
        await database.SaveChangesAsync();
        
        return RequestResult.Ok();
    }

    #endregion
    
    #region Poster
    
    public async Task<RequestResult> GetMediaPoster(long mediaId)
    {
        Database.Model.Media.Media? media = await database.Media.FirstOrDefaultAsync(x => x.Id == mediaId);
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

    public async Task<RequestResult> PutMediaPoster(long mediaId, MediaPosterRequest data)
    {
        UserValidator validator = userService.GetValidator().MustBeAdmin();
        if (!validator.IsValid)
        {
            return RequestResult.Forbidden();
        }
        
        Database.Model.Media.Media? media = await database.Media.FirstOrDefaultAsync(x => x.Id == mediaId);
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

        MediaPosterResponse returnData = new MediaPosterResponse(media.MediaPosterImage);
        return RequestResult.Ok(returnData);
    }

    public async Task<RequestResult> DeleteMediaPoster(long mediaId)
    {
        UserValidator validator = userService.GetValidator().MustBeAdmin();
        if (!validator.IsValid)
        {
            return RequestResult.Forbidden();
        }
        
        Database.Model.Media.Media? media = await database.Media.FirstOrDefaultAsync(x => x.Id == mediaId);

        if (media?.MediaPosterImage != null)
        {
            database.MediaPosterImages.Attach(media.MediaPosterImage);
            database.MediaPosterImages.Remove(media.MediaPosterImage);
            await database.SaveChangesAsync();
        }

        return RequestResult.NoContent();
    }
    
    #endregion

    #region Photos
    
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
    
    #endregion
}