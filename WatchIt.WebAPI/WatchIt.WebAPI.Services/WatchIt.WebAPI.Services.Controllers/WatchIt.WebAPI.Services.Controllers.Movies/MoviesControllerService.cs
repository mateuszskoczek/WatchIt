using Microsoft.EntityFrameworkCore;
using WatchIt.Common.Model.Genres;
using WatchIt.Common.Model.Movies;
using WatchIt.Database;
using WatchIt.Database.Model.Media;
using WatchIt.WebAPI.Services.Controllers.Common;
using WatchIt.WebAPI.Services.Utility.User;
using Genre = WatchIt.Database.Model.Common.Genre;

namespace WatchIt.WebAPI.Services.Controllers.Movies;

public class MoviesControllerService(DatabaseContext database, IUserService userService) : IMoviesControllerService
{
    #region PUBLIC METHODS

    public async Task<RequestResult> GetAll(MovieQueryParameters query)
    {
        IEnumerable<MovieResponse> data = await database.MediaMovies.Select(x => new MovieResponse(x)).ToListAsync();
        data = query.PrepareData(data);
        return RequestResult.Ok(data);
    }
    
    public async Task<RequestResult> Get(long id)
    {
        MediaMovie? item = await database.MediaMovies.FirstOrDefaultAsync(x => x.Id == id);
        if (item is null)
        {
            return RequestResult.NotFound();
        }

        MovieResponse data = new MovieResponse(item);
        return RequestResult.Ok(data);
    }
    
    public async Task<RequestResult> Post(MovieRequest data)
    {
        UserValidator validator = userService.GetValidator().MustBeAdmin();
        if (!validator.IsValid)
        {
            return RequestResult.Forbidden();
        }

        Media mediaItem = data.CreateMedia();
        await database.Media.AddAsync(mediaItem);
        await database.SaveChangesAsync();
        MediaMovie mediaMovieItem = data.CreateMediaMovie(mediaItem.Id);
        await database.MediaMovies.AddAsync(mediaMovieItem);
        await database.SaveChangesAsync();
        
        return RequestResult.Created($"movies/{mediaItem.Id}", new MovieResponse(mediaMovieItem));
    }
    
    public async Task<RequestResult> Put(long id, MovieRequest data)
    {
        UserValidator validator = userService.GetValidator().MustBeAdmin();
        if (!validator.IsValid)
        {
            return RequestResult.Forbidden();
        }
        
        MediaMovie? item = await database.MediaMovies.FirstOrDefaultAsync(x => x.Id == id);
        if (item is null)
        {
            return RequestResult.NotFound();
        }
        
        data.UpdateMediaMovie(item);
        data.UpdateMedia(item.Media);
        await database.SaveChangesAsync();
        
        return RequestResult.Ok();
    }
    
    public async Task<RequestResult> Delete(long id)
    {
        UserValidator validator = userService.GetValidator().MustBeAdmin();
        if (!validator.IsValid)
        {
            return RequestResult.Forbidden();
        }
        
        MediaMovie? item = await database.MediaMovies.FirstOrDefaultAsync(x => x.Id == id);
        if (item is null)
        {
            return RequestResult.NotFound();
        }

        database.MediaMovies.Attach(item);
        database.MediaMovies.Remove(item);
        database.MediaPosterImages.Attach(item.Media.MediaPosterImage!);
        database.MediaPosterImages.Remove(item.Media.MediaPosterImage!);
        database.MediaPhotoImages.AttachRange(item.Media.MediaPhotoImages);
        database.MediaPhotoImages.RemoveRange(item.Media.MediaPhotoImages);
        database.MediaGenres.AttachRange(item.Media.MediaGenres);
        database.MediaGenres.RemoveRange(item.Media.MediaGenres);
        database.MediaProductionCountries.AttachRange(item.Media.MediaProductionCountries);
        database.MediaProductionCountries.RemoveRange(item.Media.MediaProductionCountries);
        database.PersonActorRoles.AttachRange(item.Media.PersonActorRoles);
        database.PersonActorRoles.RemoveRange(item.Media.PersonActorRoles);
        database.PersonCreatorRoles.AttachRange(item.Media.PersonCreatorRoles);
        database.PersonCreatorRoles.RemoveRange(item.Media.PersonCreatorRoles);
        database.RatingsMedia.AttachRange(item.Media.RatingMedia);
        database.RatingsMedia.RemoveRange(item.Media.RatingMedia);
        database.ViewCountsMedia.AttachRange(item.Media.ViewCountsMedia);
        database.ViewCountsMedia.RemoveRange(item.Media.ViewCountsMedia);
        database.Media.Attach(item.Media);
        database.Media.Remove(item.Media);
        await database.SaveChangesAsync();
        
        return RequestResult.Ok();
    }

    public async Task<RequestResult> GetGenres(long movieId)
    {
        MediaMovie? item = await database.MediaMovies.FirstOrDefaultAsync(x => x.Id == movieId);
        if (item is null)
        {
            return RequestResult.NotFound();
        }

        IEnumerable<GenreResponse> genres = item.Media.MediaGenres.Select(x => new GenreResponse(x.Genre));
        return RequestResult.Ok(genres);
    }

    public async Task<RequestResult> PostGenre(long movieId, short genreId)
    {
        UserValidator validator = userService.GetValidator().MustBeAdmin();
        if (!validator.IsValid)
        {
            return RequestResult.Forbidden();
        }
        
        MediaMovie? movieItem = await database.MediaMovies.FirstOrDefaultAsync(x => x.Id == movieId);
        Genre? genreItem = await database.Genres.FirstOrDefaultAsync(x => x.Id == genreId);
        if (movieItem is null || genreItem is null)
        {
            return RequestResult.NotFound();
        }

        await database.MediaGenres.AddAsync(new MediaGenre
        {
            GenreId = genreId,
            MediaId = movieId,
        });
        
        return RequestResult.Ok();
    }
    
    public async Task<RequestResult> DeleteGenre(long movieId, short genreId)
    {
        UserValidator validator = userService.GetValidator().MustBeAdmin();
        if (!validator.IsValid)
        {
            return RequestResult.Forbidden();
        }
        
        MediaGenre? item = await database.MediaGenres.FirstOrDefaultAsync(x => x.MediaId == movieId && x.GenreId == genreId);
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
}