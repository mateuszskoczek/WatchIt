using Refit;
using WatchIt.DTO.Models.Controllers.Genres.Genre;
using WatchIt.DTO.Models.Controllers.Media.Medium.Query;
using WatchIt.DTO.Models.Controllers.Media.Medium.Request;
using WatchIt.DTO.Models.Controllers.Media.Medium.Response;
using WatchIt.DTO.Models.Controllers.Photos.Photo;
using WatchIt.DTO.Models.Generics.Image;
using WatchIt.DTO.Models.Generics.Rating;
using WatchIt.DTO.Query;

namespace WatchIt.Website.Clients;

public interface IMediaClient
{
    #region Main

    [Get("/")]
    Task<IApiResponse<IEnumerable<MediumResponse>>> GetMedia([Query(CollectionFormat.Multi)] MediumFilterQuery? filterQuery = null, [Query] OrderQuery? orderQuery = null, [Query] PagingQuery? pagingQuery = null, [Query][AliasAs("include_pictures")] bool includePictures = false);

    [Get("/{id}")]
    Task<IApiResponse<MediumResponse>> GetMedium(long id, [Query][AliasAs("include_pictures")] bool includePictures = false);

    [Get("/movies")]
    Task<IApiResponse<IEnumerable<MediumMovieResponse>>> GetMediumMovies([Query(CollectionFormat.Multi)] MediumMovieFilterQuery? filterQuery = null, [Query] OrderQuery? orderQuery = null, [Query] PagingQuery? pagingQuery = null, [Query][AliasAs("include_pictures")] bool includePictures = false);

    [Get("/movies/{id}")]
    Task<IApiResponse<MediumMovieResponse>> GetMediumMovie(long id, [Query][AliasAs("include_pictures")] bool includePictures = false);

    [Get("/series")]
    Task<IApiResponse<IEnumerable<MediumSeriesResponse>>> GetMediumSeries([Query(CollectionFormat.Multi)] MediumSeriesFilterQuery? filterQuery = null, [Query] OrderQuery? orderQuery = null, [Query] PagingQuery? pagingQuery = null, [Query][AliasAs("include_pictures")] bool includePictures = false);

    [Get("/series/{id}")]
    Task<IApiResponse<MediumSeriesResponse>> GetMediumSeries(long id, [Query][AliasAs("include_pictures")] bool includePictures = false);

    [Post("/movies")]
    Task<IApiResponse<MediumMovieResponse>> PostMediumMovie([Authorize]string token, [Body] MediumMovieRequest body);

    [Post("/series")]
    Task<IApiResponse<MediumSeriesResponse>> PostMediumSeries([Authorize]string token, [Body] MediumSeriesRequest body);

    [Put("/movies/{id}")]
    Task<IApiResponse<MediumMovieResponse>> PutMediumMovie([Authorize]string token, long id, [Body] MediumMovieRequest body);

    [Put("/series/{id}")]
    Task<IApiResponse<MediumSeriesResponse>> PutMediumSeries([Authorize]string token, long id, [Body] MediumSeriesRequest body);

    [Delete("/{id}")]
    Task<IApiResponse> DeleteMedium([Authorize]string token, long id);

    #endregion

    #region Genres

    [Get("/{id}/genres")]
    Task<IApiResponse<IEnumerable<GenreResponse>>> GetMediumGenres(long id, [Query] GenreFilterQuery? filterQuery = null, [Query] OrderQuery? orderQuery = null, [Query] PagingQuery? pagingQuery = null);

    [Post("/{id}/genres/{genre_id}")]
    Task<IApiResponse> PostMediumGenre([Authorize]string token, long id, [AliasAs("genre_id")] short genreId);

    [Delete("/{id}/genres/{genre_id}")]
    Task<IApiResponse> DeleteMediumGenre([Authorize]string token, long id, [AliasAs("genre_id")] short genreId);

    #endregion

    #region Rating

    [Get("/{id}/rating")]
    Task<IApiResponse<RatingOverallResponse>> GetMediumRating(long id);

    [Get("/{id}/rating/{account_id}")]
    Task<IApiResponse<RatingUserResponse>> GetMediumUserRating(long id, [AliasAs("account_id")] long accountId);

    [Put("/{id}/rating")]
    Task<IApiResponse> PutMediumRating([Authorize]string token, long id, [Body] RatingRequest body);

    [Delete("/{id}/rating")]
    Task<IApiResponse> DeleteMediumRating([Authorize]string token, long id);

    #endregion

    #region View Count

    [Put("/{id}/view_count")]
    Task<IApiResponse> PutMediumViewCount(long id);

    #endregion

    #region Picture

    [Get("/{id}/picture")]
    Task<IApiResponse<ImageResponse>> GetMediumPicture(long id);

    [Put("/{id}/picture")]
    Task<IApiResponse<ImageResponse>> PutMediumPicture([Authorize]string token, long id, [Body] ImageRequest body);

    [Delete("/{id}/picture")]
    Task<IApiResponse> DeleteMediumPicture([Authorize]string token, long id);

    #endregion

    #region Photos

    [Get("/{id}/photos/background")]
    Task<IApiResponse<PhotoResponse?>> GetMediumBackgroundPhoto(long id);

    #endregion
}
