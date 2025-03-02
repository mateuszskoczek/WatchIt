using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WatchIt.DTO.Models.Controllers.Genres.Genre;
using WatchIt.DTO.Models.Controllers.Media.Medium.Query;
using WatchIt.DTO.Models.Controllers.Media.Medium.Request;
using WatchIt.DTO.Models.Controllers.Media.Medium.Response;
using WatchIt.DTO.Models.Controllers.Photos.Photo;
using WatchIt.DTO.Models.Controllers.Roles.Role.Response;
using WatchIt.DTO.Models.Generics.Image;
using WatchIt.DTO.Models.Generics.Rating;
using WatchIt.DTO.Query;
using WatchIt.WebAPI.BusinessLogic.Media;
using WatchIt.WebAPI.Constants;

namespace WatchIt.WebAPI.Controllers;

[ApiController]
[Route("media")]
public class MediaController(IMediaBusinessLogic mediaBusinessLogic) : ControllerBase
{
    #region Main

    [HttpGet]
    [AllowAnonymous]
    [TranslateResultToActionResult]
    public async Task<Result<IEnumerable<MediumResponse>>> GetMedia([FromQuery] MediumFilterQuery filterQuery, [FromQuery] OrderQuery orderQuery, [FromQuery] PagingQuery pagingQuery, [FromQuery(Name = "include_pictures")]bool includePictures = false) =>
        await mediaBusinessLogic.GetMedia(filterQuery, orderQuery, pagingQuery, includePictures);

    [HttpGet("{id:long}")]
    [AllowAnonymous]
    [TranslateResultToActionResult]
    public async Task<Result<MediumResponse>> GetMedium([FromRoute(Name = "id")] long id, [FromQuery(Name = "include_pictures")]bool includePictures = false) =>
        await mediaBusinessLogic.GetMedium(id, includePictures);

    [HttpGet("movies")]
    [AllowAnonymous]
    [TranslateResultToActionResult]
    public async Task<Result<IEnumerable<MediumMovieResponse>>> GetMediumMovies([FromQuery] MediumMovieFilterQuery filterQuery, [FromQuery] OrderQuery orderQuery, [FromQuery] PagingQuery pagingQuery, [FromQuery(Name = "include_pictures")]bool includePictures = false) =>
        await mediaBusinessLogic.GetMediumMovies(filterQuery, orderQuery, pagingQuery, includePictures);

    [HttpGet("movies/{id:long}")]
    [AllowAnonymous]
    [TranslateResultToActionResult]
    public async Task<Result<MediumMovieResponse>> GetMediumMovie([FromRoute(Name = "id")] long id, [FromQuery(Name = "include_pictures")]bool includePictures = false) =>
        await mediaBusinessLogic.GetMediumMovie(id, includePictures);

    [HttpGet("series")]
    [AllowAnonymous]
    [TranslateResultToActionResult]
    public async Task<Result<IEnumerable<MediumSeriesResponse>>> GetMediumSeries([FromQuery] MediumSeriesFilterQuery filterQuery, [FromQuery] OrderQuery orderQuery, [FromQuery] PagingQuery pagingQuery, [FromQuery(Name = "include_pictures")]bool includePictures = false) =>
        await mediaBusinessLogic.GetMediumSeries(filterQuery, orderQuery, pagingQuery, includePictures);

    [HttpGet("series/{id:long}")]
    [AllowAnonymous]
    [TranslateResultToActionResult]
    public async Task<Result<MediumSeriesResponse>> GetMediumSeries([FromRoute(Name = "id")] long id, [FromQuery(Name = "include_pictures")]bool includePictures = false) =>
        await mediaBusinessLogic.GetMediumSeries(id, includePictures);

    [HttpPost("movies")]
    [Authorize(Policy = Policies.Admin)]
    [TranslateResultToActionResult]
    public async Task<Result<MediumMovieResponse>> PostMediumMovie([FromBody] MediumMovieRequest body) =>
        await mediaBusinessLogic.PostMediumMovie(body);

    [HttpPost("series")]
    [Authorize(Policy = Policies.Admin)]
    [TranslateResultToActionResult]
    public async Task<Result<MediumSeriesResponse>> PostMediumSeries([FromBody] MediumSeriesRequest body) =>
        await mediaBusinessLogic.PostMediumSeries(body);

    [HttpPut("movies/{id:long}")]
    [Authorize(Policy = Policies.Admin)]
    [TranslateResultToActionResult]
    public async Task<Result<MediumMovieResponse>> PutMediumMovie([FromRoute(Name = "id")] long id, [FromBody] MediumMovieRequest body) =>
        await mediaBusinessLogic.PutMediumMovie(id, body);

    [HttpPut("series/{id:long}")]
    [Authorize(Policy = Policies.Admin)]
    [TranslateResultToActionResult]
    public async Task<Result<MediumSeriesResponse>> PutMediumSeries([FromRoute(Name = "id")] long id, [FromBody] MediumSeriesRequest body) =>
        await mediaBusinessLogic.PutMediumSeries(id, body);

    [HttpDelete("{id:long}")]
    [Authorize(Policy = Policies.Admin)]
    [TranslateResultToActionResult]
    public async Task<Result> DeleteMedium([FromRoute(Name = "id")] long id) =>
        await mediaBusinessLogic.DeleteMedium(id);

    #endregion

    #region Genres

    [HttpGet("{id:long}/genres")]
    [AllowAnonymous]
    [TranslateResultToActionResult]
    public async Task<Result<IEnumerable<GenreResponse>>> GetMediumGenres([FromRoute(Name = "id")] long id, [FromQuery] GenreFilterQuery filterQuery, [FromQuery] OrderQuery orderQuery, [FromQuery] PagingQuery pagingQuery) =>
        await mediaBusinessLogic.GetMediumGenres(id, filterQuery, orderQuery, pagingQuery);

    [HttpPost("{id:long}/genres/{genre_id}")]
    [Authorize(Policy = Policies.Admin)]
    [TranslateResultToActionResult]
    public async Task<Result> PostMediumGenre([FromRoute(Name = "id")] long id, [FromRoute(Name = "genre_id")] short genreId) =>
        await mediaBusinessLogic.PostMediumGenre(id, genreId);

    [HttpDelete("{id:long}/genres/{genre_id}")]
    [Authorize(Policy = Policies.Admin)]
    [TranslateResultToActionResult]
    public async Task<Result> DeleteMediumGenre([FromRoute(Name = "id")] long id, [FromRoute(Name = "genre_id")] short genreId) =>
        await mediaBusinessLogic.DeleteMediumGenre(id, genreId);

    #endregion

    #region Rating

    [HttpGet("{id:long}/rating")]
    [AllowAnonymous]
    [TranslateResultToActionResult]
    public async Task<Result<RatingOverallResponse>> GetMediumRating([FromRoute(Name = "id")] long id) =>
        await mediaBusinessLogic.GetMediumRating(id);

    [HttpGet("{id:long}/rating/{account_id:long}")]
    [AllowAnonymous]
    [TranslateResultToActionResult]
    public async Task<Result<RatingUserResponse>> GetMediumUserRating([FromRoute(Name = "id")] long id, [FromRoute(Name = "account_id")] long accountId) =>
        await mediaBusinessLogic.GetMediumUserRating(id, accountId);

    [HttpPut("{id:long}/rating")]
    [Authorize]
    [TranslateResultToActionResult]
    public async Task<Result> PutMediumRating([FromRoute(Name = "id")] long id, [FromBody] RatingRequest body) =>
        await mediaBusinessLogic.PutMediumRating(id, body);

    [HttpDelete("{id:long}/rating")]
    [Authorize]
    [TranslateResultToActionResult]
    public async Task<Result> DeleteMediumRating([FromRoute(Name = "id")] long id) =>
        await mediaBusinessLogic.DeleteMediumRating(id);

    #endregion

    #region View Count

    [HttpPut("{id:long}/view_count")]
    [AllowAnonymous]
    [TranslateResultToActionResult]
    public async Task<Result> PutMediumViewCount([FromRoute(Name = "id")] long id) =>
        await mediaBusinessLogic.PutMediumViewCount(id);

    #endregion

    #region Picture

    [HttpGet("{id:long}/picture")]
    [AllowAnonymous]
    [TranslateResultToActionResult]
    public async Task<Result<ImageResponse>> GetMediumPicture([FromRoute(Name = "id")] long id) =>
        await mediaBusinessLogic.GetMediumPicture(id);

    [HttpPut("{id:long}/picture")]
    [Authorize(Policy = Policies.Admin)]
    [TranslateResultToActionResult]
    public async Task<Result<ImageResponse>> PutMediumPicture([FromRoute(Name = "id")] long id, [FromBody] ImageRequest body) =>
        await mediaBusinessLogic.PutMediumPicture(id, body);

    [HttpDelete("{id:long}/picture")]
    [Authorize(Policy = Policies.Admin)]
    [TranslateResultToActionResult]
    public async Task<Result> DeleteMediumPicture([FromRoute(Name = "id")] long id) =>
        await mediaBusinessLogic.DeleteMediumPicture(id);

    #endregion

    #region Photos

    [HttpGet("{id:long}/photos/background")]
    [AllowAnonymous]
    [TranslateResultToActionResult]
    public async Task<Result<PhotoResponse?>> GetMediumBackgroundPhoto([FromRoute(Name = "id")] long id) =>
        await mediaBusinessLogic.GetMediumPhotoBackground(id);

    #endregion
}