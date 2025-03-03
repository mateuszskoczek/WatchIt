using Ardalis.Result;
using Microsoft.EntityFrameworkCore;
using SimpleToolkit.Extensions;
using WatchIt.Database.Model.Accounts;
using WatchIt.Database.Model.Genres;
using WatchIt.Database.Model.Media;
using WatchIt.Database.Model.Photos;
using WatchIt.Database.Model.Roles;
using WatchIt.DTO.Models.Controllers.Genres;
using WatchIt.DTO.Models.Controllers.Genres.Genre;
using WatchIt.DTO.Models.Controllers.Media;
using WatchIt.DTO.Models.Controllers.Media.Medium;
using WatchIt.DTO.Models.Controllers.Media.Medium.Query;
using WatchIt.DTO.Models.Controllers.Media.Medium.Request;
using WatchIt.DTO.Models.Controllers.Media.Medium.Response;
using WatchIt.DTO.Models.Controllers.Photos;
using WatchIt.DTO.Models.Controllers.Photos.Photo;
using WatchIt.DTO.Models.Controllers.Roles;
using WatchIt.DTO.Models.Controllers.Roles.Role.Response;
using WatchIt.DTO.Models.Generics.Image;
using WatchIt.DTO.Models.Generics.Rating;
using WatchIt.DTO.Query;
using WatchIt.WebAPI.Repositories.Genres;
using WatchIt.WebAPI.Repositories.Media;
using WatchIt.WebAPI.Repositories.Photos;
using WatchIt.WebAPI.Services.User;

namespace WatchIt.WebAPI.BusinessLogic.Media;

public class MediaBusinessLogic : IMediaBusinessLogic
{
    #region SERVICES

    private readonly IUserService _userService;
    private readonly IMediaRepository _mediaRepository;
    private readonly IGenresRepository _genresRepository;
    private readonly IPhotosRepository _photosRepository;

    #endregion
    
    
    
    #region CONSTRUCTORS
    
    public MediaBusinessLogic(IUserService userService, IMediaRepository mediaRepository, IGenresRepository genresRepository, IPhotosRepository photosRepository)
    {
        _userService = userService;
        _mediaRepository = mediaRepository;
        _genresRepository = genresRepository;
        _photosRepository = photosRepository;
    }
    
    #endregion
    
    
    
    #region PUBLIC METHODS

    #region Main
    
    public async Task<Result<IEnumerable<MediumResponse>>> GetMedia(MediumFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, bool includePictures)
    {
        IEnumerable<Medium> entities = await _mediaRepository.GetAllAsync(filterQuery, orderQuery, pagingQuery, x => IncludeForMediumResponse(x, includePictures));
        return Result.Success(entities.Select(x => x.ToResponse()));
    }
    
    public async Task<Result<MediumResponse>> GetMedium(long mediumId, bool includePictures)
    {
        Medium? entity = await _mediaRepository.GetAsync(mediumId, x => IncludeForMediumResponse(x, includePictures));
        return entity switch
        {
            null => Result.NotFound(),
            _ => Result.Success(entity.ToResponse())
        };
    }
    
    public async Task<Result<IEnumerable<MediumMovieResponse>>> GetMediumMovies(MediumMovieFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, bool includePictures)
    {
        IEnumerable<MediumMovie> entities = await _mediaRepository.GetAllMoviesAsync(filterQuery, orderQuery, pagingQuery, x => IncludeForMediumResponse(x, includePictures));
        return Result.Success(entities.Select(x => x.ToResponse()));
    }
    
    public async Task<Result<MediumMovieResponse>> GetMediumMovie(long mediumId, bool includePictures)
    {
        MediumMovie? entity = await _mediaRepository.GetAsync<MediumMovie>(mediumId, x => IncludeForMediumResponse(x, includePictures));
        return entity switch
        {
            null => Result.NotFound(),
            _ => Result.Success(entity.ToResponse()),
        };
    }
    
    public async Task<Result<IEnumerable<MediumSeriesResponse>>> GetMediumSeries(MediumSeriesFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, bool includePictures)
    {
        IEnumerable<MediumSeries> entities = await _mediaRepository.GetAllSeriesAsync(filterQuery, orderQuery, pagingQuery, x => IncludeForMediumResponse(x, includePictures));
        return Result.Success(entities.Select(x => x.ToResponse()));
    }
    
    public async Task<Result<MediumSeriesResponse>> GetMediumSeries(long mediumId, bool includePictures)
    {
        MediumSeries? entity = await _mediaRepository.GetAsync<MediumSeries>(mediumId, x => IncludeForMediumResponse(x, includePictures));
        return entity switch
        {
            null => Result.NotFound(),
            _ => Result.Success(entity.ToResponse()),
        };
    }

    public async Task<Result<MediumMovieResponse>> PostMediumMovie(MediumMovieRequest body)
    {
        MediumMovie entity = body.ToEntity();
        await _mediaRepository.AddAsync(entity);
        return Result.Created(entity.ToResponse());
    }
    
    public async Task<Result<MediumSeriesResponse>> PostMediumSeries(MediumSeriesRequest body)
    {
        MediumSeries entity = body.ToEntity();
        await _mediaRepository.AddAsync(entity);
        return Result.Created(entity.ToResponse());
    }
    
    public async Task<Result<MediumMovieResponse>> PutMediumMovie(long mediumId, MediumMovieRequest body)
    {
        return await _mediaRepository.UpdateAsync<MediumMovie>(mediumId, x => x.UpdateWithRequest(body)) switch
        {
            false => Result.NotFound(),
            true => Result.Success()
        };
    }

    public async Task<Result<MediumSeriesResponse>> PutMediumSeries(long mediumId, MediumSeriesRequest body)
    {
        return await _mediaRepository.UpdateAsync<MediumSeries>(mediumId, x => x.UpdateWithRequest(body)) switch
        {
            false => Result.NotFound(),
            true => Result.Success()
        };
    }

    public async Task<Result> DeleteMedium(long mediumId)
    {
        await _mediaRepository.DeleteAsync(x => x.Id == mediumId);
        return Result.NoContent();
    }
    
    #endregion
    
    #region Genres

    public async Task<Result<IEnumerable<GenreResponse>>> GetMediumGenres(long mediumId, GenreFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery)
    {
        if (!await _mediaRepository.ExistsAsync(mediumId))
        {
            return Result.NotFound();
        }
        IEnumerable<Genre> genres = await _mediaRepository.GetMediumGenresAsync(mediumId, filterQuery, orderQuery, pagingQuery);
        return Result.Success(genres.Select(x => x.ToResponse()));
    }

    public async Task<Result> PostMediumGenre(long mediumId, short genreId)
    {
        bool mediumExists = await _mediaRepository.ExistsAsync(mediumId);
        bool genreExists = await _genresRepository.ExistsAsync(genreId);
        if (mediumExists && genreExists)
        {
            await _mediaRepository.AddMediumGenreAsync(mediumId, genreId);
            return Result.Success();
        }
        return Result.NotFound();
    }

    public async Task<Result> DeleteMediumGenre(long mediumId, short genreId)
    {
        await _mediaRepository.DeleteMediumGenreAsync(mediumId, genreId);
        return Result.NoContent();
    }
    
    #endregion
    
    #region Rating

    public async Task<Result<RatingOverallResponse>> GetMediumRating(long mediumId)
    {
        Medium? entity = await _mediaRepository.GetAsync(mediumId, x => x.Include(y => y.Ratings));
        return entity switch
        {
            null => Result.NotFound(),
            _ => Result.Success(entity.Ratings.ToOverallResponse())
        };
    }

    public async Task<Result<RatingUserResponse>> GetMediumUserRating(long mediumId, long accountId)
    {
        MediumRating? entity = await _mediaRepository.GetMediumUserRatingAsync(mediumId, accountId);
        return entity switch
        {
            null => Result.NotFound(),
            _ => Result.Success(entity.ToUserResponse())
        };
    }

    public async Task<Result> PutMediumRating(long mediumId, RatingRequest body)
    {
        Account accountEntity = await _userService.GetAccountAsync();
        Medium? mediumEntity = await _mediaRepository.GetAsync(mediumId);
        if (mediumEntity is null)
        {
            return Result.NotFound();
        }
        await _mediaRepository.UpdateOrAddMediumRatingAsync(mediumEntity.Id, accountEntity.Id, () => body.ToEntity(mediumEntity.Id, accountEntity.Id), x => x.UpdateWithRequest(body));
        return Result.Success();
    }

    public async Task<Result> DeleteMediumRating(long mediumId)
    {
        Account accountEntity = await _userService.GetAccountAsync();
        await _mediaRepository.DeleteMediumUserRatingAsync(mediumId, accountEntity.Id);
        return Result.NoContent();
    }
    
    #endregion
    
    #region View count

    public async Task<Result> PutMediumViewCount(long mediumId)
    {
        Medium? entity = await _mediaRepository.GetAsync(mediumId);
        if (entity is null)
        {
            return Result.NotFound();
        }

        DateOnly date = DateOnly.FromDateTime(DateTime.UtcNow);
        await _mediaRepository.UpdateOrAddMediumViewCountAsync(mediumId, date, () => MediaMappers.CreateMediumViewCountEntity(mediumId), x => x.ViewCount++);
        return Result.Success();
    }
    
    #endregion
    
    #region Pictures

    public async Task<Result<ImageResponse>> GetMediumPicture(long mediumId)
    {
        MediumPicture? entity = await _mediaRepository.GetMediumPictureAsync(mediumId);
        return entity switch
        {
            null => Result.NotFound(),
            _ => Result.Success(entity.ToResponse())
        };
    }

    public async Task<Result<ImageResponse>> PutMediumPicture(long mediumId, ImageRequest body)
    {
        return await _mediaRepository.ExistsAsync(mediumId) switch
        {
            true => Result.Success((await _mediaRepository.UpdateOrAddMediumPictureAsync(mediumId, () => body.ToEntity(mediumId), x => x.UpdateWithRequest(body))).ToResponse()),
            false => Result.NotFound(),
        };
    }

    public async Task<Result> DeleteMediumPicture(long mediumId)
    {
        await _mediaRepository.DeleteMediumPictureAsync(mediumId);
        return Result.NoContent();
    }
    
    #endregion
    
    #region Photos

    public async Task<Result<PhotoResponse?>> GetMediumPhotoBackground(long mediumId)
    {
        if (!await _mediaRepository.ExistsAsync(mediumId))
        {
            return Result.NotFound();
        }
        Photo photo = await _photosRepository.GetPhotoRandomBackgroundByMediumAsync(mediumId, x => x.Include(y => y.Background));
        return photo.ToResponse();
    }
    
    #endregion
    
    #endregion
    
    
    
    #region PRIVATE METHODS

    private IQueryable<T> IncludeForMediumResponse<T>(IQueryable<T> query, bool includePictures) where T : Medium
    {
        query = query.Include(y => y.Genres)
                     .Include(y => y.Ratings)
                     .Include(y => y.ViewCounts);
        if (includePictures)
        {
            query = query.Include(y => y.Picture);
        }
        return query;
    }
    
    #endregion
}