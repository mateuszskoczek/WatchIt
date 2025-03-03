using Ardalis.Result;
using WatchIt.DTO.Models.Controllers.Genres.Genre;
using WatchIt.DTO.Models.Controllers.Media.Medium.Query;
using WatchIt.DTO.Models.Controllers.Media.Medium.Request;
using WatchIt.DTO.Models.Controllers.Media.Medium.Response;
using WatchIt.DTO.Models.Controllers.Photos.Photo;
using WatchIt.DTO.Models.Controllers.Roles.Role.Response;
using WatchIt.DTO.Models.Generics.Image;
using WatchIt.DTO.Models.Generics.Rating;
using WatchIt.DTO.Query;

namespace WatchIt.WebAPI.BusinessLogic.Media;

public interface IMediaBusinessLogic
{
    #region Main

    Task<Result<IEnumerable<MediumResponse>>> GetMedia(MediumFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, bool includePictures);
    Task<Result<MediumResponse>> GetMedium(long mediumId, bool includePictures);
    Task<Result<IEnumerable<MediumMovieResponse>>> GetMediumMovies(MediumMovieFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, bool includePictures);
    Task<Result<MediumMovieResponse>> GetMediumMovie(long mediumId, bool includePictures);
    Task<Result<IEnumerable<MediumSeriesResponse>>> GetMediumSeries(MediumSeriesFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery, bool includePictures);
    Task<Result<MediumSeriesResponse>> GetMediumSeries(long mediumId, bool includePictures);
    Task<Result<MediumMovieResponse>> PostMediumMovie(MediumMovieRequest body);
    Task<Result<MediumSeriesResponse>> PostMediumSeries(MediumSeriesRequest body);
    Task<Result<MediumMovieResponse>> PutMediumMovie(long mediumId, MediumMovieRequest body);
    Task<Result<MediumSeriesResponse>> PutMediumSeries(long mediumId, MediumSeriesRequest body);
    Task<Result> DeleteMedium(long mediumId);

    #endregion
    
    #region Genres
    
    Task<Result<IEnumerable<GenreResponse>>> GetMediumGenres(long mediumId, GenreFilterQuery filterQuery, OrderQuery orderQuery, PagingQuery pagingQuery);
    Task<Result> PostMediumGenre(long mediumId, short genreId);
    Task<Result> DeleteMediumGenre(long mediumId, short genreId);
    
    #endregion

    #region Rating

    Task<Result<RatingOverallResponse>> GetMediumRating(long mediumId);
    Task<Result<RatingUserResponse>> GetMediumUserRating(long mediumId, long accountId);
    Task<Result> PutMediumRating(long mediumId, RatingRequest body);
    Task<Result> DeleteMediumRating(long mediumId);

    #endregion
    
    #region View count
    
    Task<Result> PutMediumViewCount(long mediumId);
    
    #endregion
    
    #region Picture
    
    Task<Result<ImageResponse>> GetMediumPicture(long mediumId);
    Task<Result<ImageResponse>> PutMediumPicture(long mediumId, ImageRequest body);
    Task<Result> DeleteMediumPicture(long mediumId);
    
    #endregion
    
    #region Photos
    
    Task<Result<PhotoResponse?>> GetMediumPhotoBackground(long mediumId);
    
    #endregion
}