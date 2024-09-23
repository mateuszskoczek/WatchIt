using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WatchIt.Common.Model.Genres;
using WatchIt.Common.Model.Media;
using WatchIt.WebAPI.Services.Controllers.Media;

namespace WatchIt.WebAPI.Controllers;

[ApiController]
[Route("media")]
public class MediaController(IMediaControllerService mediaControllerService)
{
    #region MAIN
    
    [HttpGet("{id}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(MediaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetMedia([FromRoute] long id) => await mediaControllerService.GetMedia(id);
    
    #endregion
    
    
    
    #region GENRES
    
    [HttpGet("{id}/genres")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<GenreResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetMediaGenres([FromRoute]long id) => await mediaControllerService.GetMediaGenres(id);
    
    [HttpPost("{id}/genres/{genre_id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> PostMediaGenre([FromRoute]long id, [FromRoute(Name = "genre_id")]short genreId) => await mediaControllerService.PostMediaGenre(id, genreId);
    
    [HttpDelete("{id}/genres/{genre_id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteMediaGenre([FromRoute]long id, [FromRoute(Name = "genre_id")]short genreId) => await mediaControllerService.DeleteMediaGenre(id, genreId);
    
    #endregion



    #region RATING

    [HttpGet("{id}/rating")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(MediaRatingResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetMediaRating([FromRoute] long id) => await mediaControllerService.GetMediaRating(id);
    
    [HttpGet("{id}/rating/{user_id}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(short), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetMediaRatingByUser([FromRoute] long id, [FromRoute(Name = "user_id")]long userId) => await mediaControllerService.GetMediaRatingByUser(id, userId);
    
    [HttpPut("{id}/rating")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> PutMediaRating([FromRoute] long id, [FromBody] MediaRatingRequest data) => await mediaControllerService.PutMediaRating(id, data);
    
    [HttpDelete("{id}/rating")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> DeleteMediaRating([FromRoute] long id) => await mediaControllerService.DeleteMediaRating(id);
    
    #endregion



    #region VIEW COUNT

    [HttpPost("{id}/view")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> PostMediaView([FromRoute] long id) => await mediaControllerService.PostMediaView(id);

    #endregion
    
    
    
    #region POSTER
    
    [HttpGet("{id}/poster")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(MediaPosterResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetMediaPoster([FromRoute] long id) => await mediaControllerService.GetMediaPoster(id);
    
    [HttpPut("{id}/poster")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(MediaPosterResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> PutMediaPoster([FromRoute]long id, [FromBody]MediaPosterRequest body) => await mediaControllerService.PutMediaPoster(id, body);
    
    [HttpDelete("{id}/poster")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> DeleteMediaPoster([FromRoute]long id) => await mediaControllerService.DeleteMediaPoster(id);
    
    #endregion
    
    
    
    #region PHOTOS
    
    [HttpGet("{id}/photos/random_background")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(MediaPhotoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetMediaPhotoRandomBackground([FromRoute]long id) => await mediaControllerService.GetMediaRandomBackgroundPhoto(id);
    
    [HttpGet("photos/{photo_id}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(MediaPhotoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetPhoto([FromRoute(Name = "photo_id")] Guid photoId) => await mediaControllerService.GetPhoto(photoId);

    [HttpGet("photos")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<MediaPhotoResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetPhotos(MediaPhotoQueryParameters query) => await mediaControllerService.GetPhotos(query);

    [HttpGet("photos/random_background")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(MediaPhotoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetPhotoRandomBackground() => await mediaControllerService.GetRandomBackgroundPhoto();
    
    [HttpPost("photos")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(MediaPhotoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> PostPhoto([FromBody]MediaPhotoRequest body) => await mediaControllerService.PostPhoto(body);
    
    [HttpPut("photos/{photo_id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> PutPhoto([FromRoute(Name = "photo_id")]Guid photoId, [FromBody]MediaPhotoRequest body) => await mediaControllerService.PutPhoto(photoId, body);
    
    [HttpDelete("photos/{photo_id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeletePhoto([FromRoute(Name = "photo_id")]Guid photoId) => await mediaControllerService.DeletePhoto(photoId);
    
    #endregion
}